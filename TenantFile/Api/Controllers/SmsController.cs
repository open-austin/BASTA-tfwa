using System;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using MimeTypes;
using System.IO;
using Microsoft.Extensions.Primitives;
using System.Net;
using Newtonsoft.Json;
using TenantFile.Api.Services;

namespace TenantFile.Api.Controllers
{
    public class SmsController : TwilioController
    {
        private readonly ILogger<SmsController> _logger;
        private readonly ICloudStorage _storageClient;

        public SmsController(ILogger<SmsController> logger, ICloudStorage storageClient)
        {
            _logger = logger;
            _storageClient = storageClient;
        }

        [HttpPost("/api/sms")]
        public async Task<TwiMLResult> SmsWebhook(SmsRequest request, int numMedia)
        {

            // _logger.LogDebug(JsonConvert.SerializeObject(request, Formatting.Indented));
            // _logger.LogDebug(JsonConvert.SerializeObject(Request.Form, Formatting.Indented, new JsonSerializerSettings
            // {
            //     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            // }));
            // Does this contain media?

            for (var i = 0; i < numMedia; i++)
            {
                var mediaUrl = Request.Form[$"MediaUrl{i}"];
                _logger.LogInformation(mediaUrl);
                var contentType = Request.Form[$"MediaContentType{i}"];

                var filePath = Path.Combine("images", GetMediaFileName(mediaUrl, contentType));
                await _storageClient.UploadToStorageAsync(mediaUrl, filePath, contentType);
            }

            var response = new MessagingResponse();
            var messageBody = numMedia == 0 ? "Send us an image!" :
                $"Thanks for sending us {numMedia} file(s)!";
            response.Message(messageBody);
            return TwiML(response);
        }

        // private async Task UploadToStorageAsync(string fileUrl, string filePath, string contentType)
        // {
        //     WebClient webClient = new WebClient();
        //     var stream = webClient.OpenRead(fileUrl);

        //     await _storageClient.UploadObjectAsync("tenant-file-fc6de.appspot.com", $"images/{filePath}", contentType, stream);
        // }

        private string GetMediaFileName(string mediaUrl,
            string contentType)
        {
            return Path.GetFileName(mediaUrl) + MimeTypeMap.GetExtension(contentType);
        }
    }
}