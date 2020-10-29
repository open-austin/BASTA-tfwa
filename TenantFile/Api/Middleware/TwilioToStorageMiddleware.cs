using HotChocolate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
using System.Threading.Tasks;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models;
using TenantFile.Api.Services;

namespace TenantFile.Api.Middleware
{
    public class TwilioToStorageMiddleware
    {
        private readonly ILogger<TwilioToStorageMiddleware> logger;
        private readonly RequestDelegate _next;
        //private readonly TenantFileContext dbContext;
        private readonly ICloudStorage storageClient;

        
        public TwilioToStorageMiddleware(ILogger<TwilioToStorageMiddleware> logger, RequestDelegate next, ICloudStorage storageClient)
        {
            this.logger = logger;
            _next = next;
            //this.dbContext = dbContext;
            this.storageClient = storageClient;
        }
        [UseTenantFileContext]
        public async Task Invoke(HttpContext httpContext, [ScopedService] TenantFileContext dbContext)
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

                        foreach (var (image, thumbnail) in filenames)
                        {
                            if (phone.Images == null)
                            {
                                phone.Images = new List<Models.Image>();
                            }
                            phone.Images.Add(new Models.Image
                            {
                                Name = image,
                                ThumbnailName = thumbnail
                            });
                        }

                        dbContext.SaveChanges();


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

                async Task<IEnumerable<(string image, string thumbnail)>> SaveMedia(int numMedia)
                {
                    var httpClient = new HttpClient();

                    var filenames = new List<(string, string)>();

                    for (var i = 0; i < numMedia; i++)
                    {
                        var mediaUrl = httpContext.Request.Query[$"MediaUrl{i}"];
                        logger.LogInformation(mediaUrl);
                        var contentType = httpContext.Request.Query[$"MediaContentType{i}"];

                       
                        //var filePath = Path.Combine("images", GetMediaFileName(mediaUrl, contentType)).Replace("\\", "/");
                        
                        var imagePath = $"images/{mediaUrl}{GetMediaFileName(mediaUrl, contentType)}";
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
                                                
                        //var thumbnailName = Path.Combine("thumbnails", GetMediaFileName(mediaUrl, contentType)).Replace("\\", "/");

                        var thumbPath = $"thumbnails/{mediaUrl}{GetMediaFileName(mediaUrl, contentType)}";
                        await storageClient.UploadStreamToStorageAsync(outputStream, thumbPath, "image/png");

                        filenames.Add((imagePath, thumbPath));
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
