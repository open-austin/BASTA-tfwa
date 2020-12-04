using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using MimeTypes;
using System.IO;
using TenantFile.Api.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TenantFile.Api.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;
using System.Net.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Subscriptions;
using HotChocolate;
using TenantFile.Api.Models.Phones;
using TenantFile.Api.Models.Entities;
using Google.Cloud.Vision.V1;
using System;

namespace TenantFile.Api.Controllers
{
    public class SmsController : TwilioController
    {
        private readonly ITopicEventSender eventSender;
        private readonly ILogger<SmsController> logger;
        private readonly IDbContextFactory<TenantFileContext> dbContextFactory;
        private readonly ICloudStorage storageClient;

        public SmsController(ILogger<SmsController> logger, ICloudStorage storageClient, IConfiguration configuration, IDbContextFactory<TenantFileContext> dbContextFactory, [Service] ITopicEventSender eventSender)
        {
            this.eventSender = eventSender;
            this.logger = logger;
            this.storageClient = storageClient;
            this.dbContextFactory = dbContextFactory;
        }

        //Twilio can use the graphql endpoint with middleware but it would effect every call to API...
        //CANNOT inject ITopicEventSending in this method
        [HttpPost("/api/sms")]
        public async Task<TwiMLResult> SmsWebhook(SmsRequest request, int numMedia/*, [Service] ITopicEventSender eventSender*/)
        {

            DateTime timeStamp = DateTime.Now;
            var filenames = await SaveMediaAsync(numMedia);

            await using TenantFileContext dbContext = dbContextFactory.CreateDbContext();
            var phone = dbContext.Phones.FirstOrDefault(x => x.PhoneNumber == request.From);

            bool newPhone = false;

            if (phone == null)
            {
                newPhone = true;//not raising the subscription event here because the image names are not populated yet.
                phone = new Phone
                {
                    PhoneNumber = request.From,
                    Images = new List<Models.Entities.Image>()
                };
                dbContext.Phones.Add(phone);
            //await dbContext.SaveChangesAsync();//This call is needed to generate the Id for the phone so it is available to the TenantEvent...but, if the TenantEvent has a navigation property to the Phone, the Id would be added when its saved later...the reference should be enough to establish the relationship
            }
            var tenantEvent = CreateTenantEventAsync(dbContext, phone, timeStamp);

            foreach (var (image, thumbnail, labels) in filenames)
            {
                if (phone.Images == null)
                {
                    phone.Images = new List<Models.Entities.Image>();
                }
                phone.Images.Add(new Models.Entities.Image
                {
                    Name = image,
                    ThumbnailName = thumbnail,
                    Labels = labels

                });
            }
            //dbContext.TenantEvents.Add(await tenantEvent);
            await dbContext.SaveChangesAsync();

            if (newPhone == true)
            {
                await eventSender.SendAsync(nameof(PhoneSubscriptions.OnNewPhoneReceived), phone.Id);//ConfigureAwait?
            }
            var response = new MessagingResponse();
            var messageBody = numMedia == 0 ? "No images were recieved from your message. Please send us an image of what you are trying to document." :
                $"Thanks for sending us {numMedia} file(s)!";
            response.Message(messageBody);
            return TwiML(response);
        }



        async Task<IEnumerable<(string image, string thumbnail, ImageLabel[] labels)>> SaveMediaAsync(int numMedia)
        {
            var filenames = new List<(string, string, ImageLabel[])>();
            for (var i = 0; i < numMedia; i++)
            {
                var mediaUrl = Request.Form[$"MediaUrl{i}"];
                var imageLabel = GetLabelsAsync(mediaUrl);
                logger.LogInformation(mediaUrl);
                var contentType = Request.Form[$"MediaContentType{i}"];

                var imagePath = $"images/{GetMediaFileName(mediaUrl, contentType)}";

                await storageClient.UploadToStorageAsync(mediaUrl, imagePath, contentType);

                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(mediaUrl);
                var inputStream = await response.Content.ReadAsStreamAsync();

                using var image = SixLabors.ImageSharp.Image.Load(inputStream);
                image.Mutate(x => x
                     .Resize(new ResizeOptions()
                     {
                         Mode = ResizeMode.Crop,
                         Size = new Size(100, 60)

                     }));

                var outputStream = new MemoryStream();
                image.Save(outputStream, encoder: new PngEncoder() { CompressionLevel = PngCompressionLevel.BestSpeed });

                var thumbPath = $"thumbnails/{GetMediaFileName(mediaUrl, contentType)}";

                await storageClient.UploadStreamToStorageAsync(outputStream, thumbPath, "image/png");

                filenames.Add((imagePath, thumbPath, (await imageLabel).ToArray()));
            }

            return filenames;
        }

        async Task<TenantEvent> CreateTenantEventAsync(TenantFileContext dbContext, Phone phone, DateTime time)
        {
            var tenant = await dbContext.Tenants.AsAsyncEnumerable()
                                                .Where(t => t.Phones.Select(p => p.PhoneNumber)
                                                .Contains(phone.PhoneNumber))
                                                .FirstOrDefaultAsync();
            return new TenantEvent()
            {
                TimeOccurred = time,
                Tenant = tenant,
                EventType = TenantEventType.SentEvidence,
                Phone = phone
            };
        }

        async Task<IEnumerable<ImageLabel>> GetLabelsAsync(string uri)
        {
            var image = Google.Cloud.Vision.V1.Image.FromUri(uri);
            var client = ImageAnnotatorClient.Create();
            var imageLables = new List<ImageLabel>();
            var response = client.DetectLabelsAsync(image);

            //definitions for safe-search
            //https://cloud.google.com/vision/docs/reference/rpc/google.cloud.vision.v1?hl=it#google.cloud.vision.v1.SafeSearchAnnotation 
            var safeResponse = client.DetectSafeSearchAsync(image);

            var visionTasks = new List<Task> { response, safeResponse };

            while (visionTasks.Count > 0)
            {
                var completedTask = await Task.WhenAny(visionTasks);

                if (completedTask == response)
                {
                    foreach (var annotation in response.Result)
                    {
                        if (annotation.Description != null)
                        {
                            imageLables.Add(new ImageLabel("GCPImageAnnotator", annotation.Description, annotation.Score));
                        }
                    }
                }
                else if (completedTask == safeResponse)
                {

                    imageLables.Add(new ImageLabel("SafeSearch Adult", safeResponse.Result.Adult.ToString()));
                    imageLables.Add(new ImageLabel("SafeSearch Violence", safeResponse.Result.Violence.ToString()));
                    imageLables.Add(new ImageLabel("SafeSearch Spoof", safeResponse.Result.Spoof.ToString()));
                    imageLables.Add(new ImageLabel("SafeSearch Racy", safeResponse.Result.Racy.ToString()));
                    imageLables.Add(new ImageLabel("SafeSearch Medical", safeResponse.Result.Medical.ToString()));
                }
                visionTasks.Remove(completedTask);
            }
            return imageLables;
        }

        //TODO: Recheck performance with long list of keywords and make Async if performance is poor
        /// <summary>
        /// Uses the return type TextAnnotation from Google's OCR search for a set of words and return the confidence that the reading was correct.
        /// Confidence is the highest found confidence for the specific term
        /// </summary>
        /// <param name="text"></param>
        /// <param name="terms"></param>
        /// <returns></returns>
        IEnumerable<ImageLabel> FindWordsFromOCR(TextAnnotation text, IEnumerable<string> terms)
        {
            var list = new List<ImageLabel>();
            foreach (var term in terms)
            {

                list.AddRange(text.Pages
                              .SelectMany(p => p.Blocks
                              .SelectMany(b => b.Paragraphs
                              .SelectMany(p => p.Words
                              .Select(w => (Text: string.Join("", w.Symbols.Select(s => s.Text)), Confidence: w.Confidence))
                              ))).Where(str => str.Text.ToLower() == term.ToLower())
                              .GroupBy(x => x.Text)
                              .Select(g => new ImageLabel( Label : g.Key, Confidence: g.Select(p => p.Confidence).Max(), Source: "GoogleOCR"))
                              );
            }
          
            return list;

        }
        string GetMediaFileName(string mediaUrl, string contentType)
        {
            return System.IO.Path.GetFileName(mediaUrl) + MimeTypeMap.GetExtension(contentType);
        }
    }
}