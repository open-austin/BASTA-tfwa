using Google.Cloud.Language.V1;
using Google.Cloud.Vision.V1;
using HotChocolate;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using MimeTypes;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TenantFile.Api.Extensions;
using TenantFile.Api.Migrations;
using TenantFile.Api.Models;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Models.Phones;
using TenantFile.Api.Services;

namespace TenantFile.Api.Middleware
{
    public class TwilioToStorageMiddleware
    {
        private readonly ILogger<TwilioToStorageMiddleware> logger;
        private readonly RequestDelegate _next;
        //private readonly TenantFileContext dbContext;
        private readonly IDbContextFactory<TenantFileContext> dbContextFactory;
        private readonly ICloudStorage storageClient;


        public TwilioToStorageMiddleware(IDbContextFactory<TenantFileContext> dbContextFactory, ILogger<TwilioToStorageMiddleware> logger, RequestDelegate next, ICloudStorage storageClient)
        {
            this.dbContextFactory = dbContextFactory;
            this.logger = logger;
            _next = next;
            //this.dbContext = dbContext;
            this.storageClient = storageClient;

        }

        public async Task Invoke(HttpContext httpContext, [Service] ITopicEventSender eventSender)
        {
            httpContext.Request.Query.TryGetValue("From", out StringValues twilioFrom);

            string messageBody;

            if (twilioFrom.FirstOrDefault() != null /*== "+12029292080"*/)
            {
                //var response = new MessagingResponse();

                httpContext.Request.Query.TryGetValue("NumMedia", out StringValues num);

                if (!int.TryParse(num, out int numMedia))
                {
                    messageBody = "NumMedia property not found";
                }
                else
                {
                    if (numMedia > 0)
                    {
                        var filenames = await SaveMedia(numMedia);

                        await using TenantFileContext dbContext = dbContextFactory.CreateDbContext();
                        var phone = dbContext.Phones.FirstOrDefault(x => x.PhoneNumber == twilioFrom.First());

                        if (phone == null)
                        {
                            phone = new Phone
                            {
                                PhoneNumber = twilioFrom.First(),
                                Images = new List<Models.Image>()
                            };
                            dbContext.Phones.Add(phone);
                        }

                        foreach (var (image, thumbnail, lab) in filenames)
                        {
                            if (phone.Images == null)
                            {
                                phone.Images = new List<Models.Image>();
                            }
                            phone.Images.Add(new Models.Image
                            {
                                Name = image,
                                ThumbnailName = thumbnail,
                                Labels = lab

                            });
                        }

                        await dbContext.SaveChangesAsync();
                        await eventSender.SendAsync(
       nameof(PhoneSubscriptions.OnNewPhoneReceivedAsync),
       phone.Id);//TODO: Does this id exist yet in memory? It is generated on creation... could alter this to generate guid in ctor


                        messageBody = $"Thanks for sending us {numMedia} file(s)!";

                    }
                    else
                    {
                        messageBody = "No images were recieved from your message. Please send us an image of what you are trying to document.";
                    }

                }
                //response.Message(body: messageBody);

                /*return */ //no need for returning here... we want the HTTP request to respond and not go further into the pipeline at this point
                            // new TwiMLResult(response);
                            //When a delegate doesn't pass a request to the next delegate, it's called short-circuiting the request pipeline.Short - circuiting is often desirable because it avoids unnecessary work.For example, Static File Middleware can act as a terminal middleware by processing a request for a static file and short-circuiting the rest of the pipeline.

                //await httpContext.Response.WriteAsJsonAsync(response);
                await httpContext.Response.WriteAsync($"<?xml version=\"1.0\" encoding=\"UTF - 8\"?>\n<Response>\n<Message>{messageBody}</Message>\n</Response>");
                #region local function SaveMedia

                async Task<IEnumerable<(string image, string thumbnail, ImageLabel[] labels)>> SaveMedia(int numMedia)
                {

                    var httpClient = new HttpClient();

                    var filenames = new List<(string, string, ImageLabel[])>();

                    for (var i = 0; i < numMedia; i++)
                    {
                        var mediaUrl = httpContext.Request.Query[$"MediaUrl{i}"];

                        var imageLabel = await GetLabelsAsync(mediaUrl);

                        logger.LogInformation(mediaUrl);

                        var contentType = httpContext.Request.Query[$"MediaContentType{i}"];

                        var imagePath = $"images/{GetMediaFileName(mediaUrl, contentType)}";
                        await storageClient.UploadToStorageAsync(mediaUrl, imagePath, contentType);

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

                        filenames.Add((imagePath, thumbPath, imageLabel.ToArray()));
                    }

                    return filenames;
                }
                #endregion
            }

            else
            {
                await _next(httpContext);
            }
        }

        async Task<IEnumerable<ImageLabel>> GetLabelsAsync(string uri)
        {
            var image = Google.Cloud.Vision.V1.Image.FromUri(uri);
            var client = ImageAnnotatorClient.Create();
            var response = await client.DetectLabelsAsync(image);
            var imageLables = new List<ImageLabel>();

            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                {
                    imageLables.Add(new ImageLabel(annotation.Description, annotation.Score));
                }
            }
            return imageLables;
        }

        private string GetMediaFileName(string mediaUrl,
            string contentType)
        {
            return System.IO.Path.GetFileName(mediaUrl) + MimeTypeMap.GetExtension(contentType);
        }

    }
    public static class TwilioGraphQLMiddlewareExtensions
    {
        public static IApplicationBuilder UseTwilioToStorage(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TwilioToStorageMiddleware>();
        }
    }
}
