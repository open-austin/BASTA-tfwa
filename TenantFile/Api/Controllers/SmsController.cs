using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
using System.Text.Json;
using TenantFile.Api.Common;

namespace TenantFile.Api.Controllers
{
    public class SmsController : Controller
    {
        private readonly ITopicEventSender eventSender;
        private readonly ILogger<SmsController> logger;
        private readonly TenantFileContext context;
        private readonly ICloudStorage storageClient;
        private readonly GoogleDriveService driveService;
        private readonly List<string> keyWords;
        private readonly List<string> keyPhrases;


        public SmsController(ILogger<SmsController> logger, ICloudStorage storageClient, IConfiguration configuration, TenantFileContext context, [Service] ITopicEventSender eventSender, GoogleDriveService driveService)
        {
            this.eventSender = eventSender;
            this.logger = logger;
            this.storageClient = storageClient;
            this.context = context;
            this.keyWords = new List<string>() { };
            this.keyPhrases = new List<string>() { "written request for repairs" };
            this.driveService = driveService;
        }
        [HttpPost("/api/captureData")]
        public async Task<IActionResult> CaptureTenantData([FromBody] FlowData data)
        {
            var phone = context.Phones.FirstOrDefault(x => x.PhoneNumber == data.from)!;

            var property = context.Properties.Include(p => p.Address).AsQueryable().Where(p => p.Name == data.propertyName).First();

            var newTenant = GetOrCreateTenant(out var tenant, phone);
            if (newTenant)
            {
                tenant.Name = string.Join(" ", data.firstName, data.lastName);
                tenant.CurrentResidence = new Residence()
                {
                    Address = new Address()
                    {
                        Line1 = property.Address.Line1,
                        Line2 = data.unitNum,
                        City = property.Address.City,
                        State = property.Address.State,
                        PostalCode = data.zip
                    },
                    Property = property,
                    PropertyId = property.Id
                };
            }
            else
            {
                //TODO: Issue 172, if the Tenant is not new, that means the Tenant asked to update/change their contact info. Should this:
                // A: overwrite the contact info
                // B: add a new Tenant and associate it with the texting Phone Entity

            }
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("/api/survey")]
        public async Task<IActionResult> SurveyWebhook()
        {
            var request = await JsonSerializer.DeserializeAsync<IDictionary<String, JsonElement>>(Request.Body);

            int numMedia = int.Parse((request!["NumMedia"]).GetString()!);
            var phoneNumber = request["From"].GetString()!;

            var filenames = new List<(string, string, ImageLabel[])>();

            for (var i = 0; i < Math.Min(numMedia, 10); i++)
            {
                var imageUrl = request[$"MediaUrl{numMedia - 1}"].GetString()!;
                var imageType = request[$"MediaContentType{numMedia - 1}"].GetString()!;
                var progress = await driveService.UploadToSurveyFolder(imageUrl, imageType, phoneNumber);

            }
            return Ok();
        }

        [HttpPost("/api/sms")]
        public async Task<IActionResult> SmsWebhook()
        {
            var request = await JsonSerializer.DeserializeAsync<IDictionary<String, JsonElement>>(Request.Body);

            int numMedia = int.Parse((request!["NumMedia"]).GetString()!);

            DateTime timeStamp = DateTime.Now;
            var filenames = await SaveMediaAsync(numMedia, request);
            var imageUrlList = new List<string>();
            var imageTypeList = new List<string>();

            var newPhone = GetOrCreatePhone(out var phone, request["From"].GetString()!);

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
            await context.SaveChangesAsync();

            if (newPhone == true)
            {
                await eventSender.SendAsync(nameof(PhoneSubscriptions.OnNewPhoneReceived), phone.Id);//ConfigureAwait?
            }
            var labelsCollection = filenames.SelectMany(t => t.labels.Select(l => l.Label));

            var currentImageIsWRR = labelsCollection.Contains("Written Request for Repairs",
                  StringComparer.InvariantCultureIgnoreCase);

            var flowType = SetFlowType(labelsCollection);
            if (newPhone)
            {
                return Json(new { type = flowType, imageNumber = numMedia, newPhone = newPhone, hasWRR = false }); //if multiple images...handle this first then circle back
            }
            else
            {
                return Json(new
                {

                    language = phone.PreferredLanguage ?? PreferredLanguage.En,
                    newPhone = newPhone,
                    type = flowType,
                    name = GetFirstNamesForPhone(phone),
                    hasWRR = await HasLabel(phone, "written request for repairs").ConfigureAwait(false)

                });
            }
        }



        async Task<IEnumerable<(string image, string thumbnail, ImageLabel[] labels)>> SaveMediaAsync(int numMedia, IDictionary<String, JsonElement> jsonBody)
        {
            var filenames = new List<(string, string, ImageLabel[])>();
            for (var i = 0; i < Math.Min(numMedia, 10); i++)
            {
                var imageUrl = jsonBody[$"MediaUrl{numMedia - 1}"].GetString()!;
                var imageType = jsonBody[$"MediaContentType{numMedia - 1}"].GetString()!;

                var imageLabel = GetLabelsAsync(imageUrl);

                // List for Phrases identified as needing attention, used by OCR matching
                logger.LogInformation(imageUrl);

                var imagePath = $"images/{GetMediaFileName(imageUrl, imageType)}";

                await storageClient.UploadToStorageAsync(imageUrl, imagePath, imageType);

                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(imageUrl);
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

                var thumbPath = $"thumbnails/{GetMediaFileName(imageUrl, imageType)}";

                await storageClient.UploadStreamToStorageAsync(outputStream, thumbPath, "image/png");
                var imageResult = await imageLabel;
                filenames.Add((imagePath, thumbPath, (imageResult).ToArray()));



            }

            return filenames;
        }

        async Task<IEnumerable<ImageLabel>> GetLabelsAsync(string uri)
        {
            var image = await Google.Cloud.Vision.V1.Image.FetchFromUriAsync(uri);//is image not avalible?  
            var client = ImageAnnotatorClient.Create();
            var imageLabels = new List<ImageLabel>();

            //definitions for safe-search
            //https://cloud.google.com/vision/docs/reference/rpc/google.cloud.vision.v1?hl=it#google.cloud.vision.v1.SafeSearchAnnotation 
            var safeResponse = client.DetectSafeSearchAsync(image);
            var labelResponse = client.DetectLabelsAsync(image);


            var visionTasks = new List<Task> { labelResponse, safeResponse };

            while (visionTasks.Count > 0)
            {
                var completedTask = await Task.WhenAny(visionTasks);

                if (completedTask == labelResponse)
                {
                    foreach (var annotation in labelResponse.Result)//could not access URL from twilio
                    {
                        if (annotation.Description != null)
                        {
                            imageLabels.Add(new ImageLabel("GCPImageAnnotator", annotation.Description, annotation.Score));
                        }
                    }
                }

                else if (completedTask == safeResponse)
                {

                    imageLabels.Add(new ImageLabel("SafeSearch Adult", safeResponse.Result.Adult.ToString()));
                    imageLabels.Add(new ImageLabel("SafeSearch Violence", safeResponse.Result.Violence.ToString()));
                    imageLabels.Add(new ImageLabel("SafeSearch Spoof", safeResponse.Result.Spoof.ToString()));
                    imageLabels.Add(new ImageLabel("SafeSearch Racy", safeResponse.Result.Racy.ToString()));
                    imageLabels.Add(new ImageLabel("SafeSearch Medical", safeResponse.Result.Medical.ToString()));
                }
                visionTasks.Remove(completedTask);
            }
            var labelDefs = imageLabels.Select(l => l.Label);
            if (labelDefs.Contains("Text") || labelDefs.Contains("Document"))
            {
                var textResponse = await client.DetectDocumentTextAsync(image);
                imageLabels.AddRange(FindWordsFromOCR(textResponse, keyWords));
                imageLabels.AddRange(FindPhrasesFromOCR(textResponse, keyPhrases));

            }

            return imageLabels;
        }

        //TODO: stil to implement, need bucket for sequestered content, delete,send custom message though Twilio 
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
            if (text != null)
            {
                foreach (var term in terms)
                {

                    list.AddRange(text.Pages
                                  .SelectMany(p => p.Blocks
                                  .SelectMany(b => b.Paragraphs
                                  .SelectMany(p => p.Words
                                  .Select(w => (Text: string.Join("", w.Symbols.Select(s => s.Text)), Confidence: w.Confidence))
                                  ))).Where(str => str.Text.ToLower() == term.ToLower())
                                  .GroupBy(x => x.Text)
                                  .Select(g => new ImageLabel(Label: g.Key, Confidence: g.Select(p => p.Confidence).Max(), Source: "KeyWord"))
                                  );
                }
            }
            return list;

        }
        public static IEnumerable<ImageLabel> FindPhrasesFromOCR(TextAnnotation text, IEnumerable<string> phrases)
        {
            var list = new List<ImageLabel>();
            if (text != null)
            {
                foreach (var term in phrases)
                {
                    list.AddRange(text.Pages
                                  .SelectMany(p => p.Blocks
                                  .SelectMany(b => b.Paragraphs
                                  .Select(w => (Text: string.Join(" ", w.Words.Select(w => string.Join("", w.Symbols.Select(s => s.Text)))), Confidence: w.Confidence)
                                  ))).Where(str => str.Text.ToLower() == term.ToLower())
                                  .GroupBy(x => x.Text)
                                  .Select(g => new ImageLabel(Label: g.Key, Confidence: g.Select(p => p.Confidence).Max(), Source: "KeyPhrase")));
                }
            }
            return list;
        }
        string GetMediaFileName(string mediaUrl, string contentType)
        {
            return System.IO.Path.GetFileName(mediaUrl) + MimeTypeMap.GetExtension(contentType);
        }
        FlowPath SetFlowType(IEnumerable<string> labels) =>

            labels switch
            {
                IEnumerable<string> s when
                 s.Contains("Written Request for Repairs",
                  StringComparer.InvariantCultureIgnoreCase)
                    => FlowPath.WrittenRepairRequest,

                IEnumerable<string> s when s.Any() => FlowPath.Image,


                _ => FlowPath.NoImage
            };

        /// <summary>
        /// If from param is found in the database, return true and assign found phone entity to the out variable
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        bool GetOrCreatePhone(out Phone phone, string fromNumber)
        {
            var newPhone = false;
            phone = context.Phones.FirstOrDefault(x => x.PhoneNumber == fromNumber)!;

            if (phone == null)
            {
                newPhone = true;
                phone = new Phone
                {
                    PhoneNumber = fromNumber,
                    Images = new List<Models.Entities.Image>()
                };
                context.Phones.Add(phone);

            }

            return newPhone;
        }
        bool GetOrCreateTenant(out Tenant tenant, Phone phone)
        {
            var newTenant = false;
            tenant = context.Tenants.FirstOrDefault(t => t.Phones.Contains(phone))!;

            if (tenant == null)
            {
                newTenant = true;
                tenant = new Tenant
                {
                    Phones = new List<Phone>() { phone }

                };
                context.Tenants.Add(tenant);

            }

            return newTenant;
        }



        /// <summary>
        /// Since names are a single string, i.e. no first or last name properties /and phone numbers can have multiple Tenants, this query will return the first word before white space for each Tenant and seperate them with an "&"
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        string GetFirstNamesForPhone(Phone phone)
        {
            return String.Join("", String.Join(" & ",
                     context.Phones.AsQueryable()
                                   .Where(p => p.Id == phone.Id)
                                   .SelectMany(p => p.Tenants.Select(i => i.Name.Split().FirstOrDefault()))));
        }

        Task<bool> HasLabel(Phone phone, string label)
        {
            var labelList = context.Phones.AsQueryable().Include(p => p.Images)
                                   .Where(p => p.Id == phone.Id)
                                   .SelectMany(p => p.Images.SelectMany(i => i.Labels!.Select(l => l.Label.ToLower())));
            return labelList.ContainsAsync(label);
        }

    }

}
