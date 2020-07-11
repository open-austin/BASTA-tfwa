using System;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
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
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TenantFile.Api.Extentions;

namespace TenantFile.Api.Controllers
{
    public class SmsController : TwilioController
    {
        private readonly ILogger<SmsController> _logger;
        private readonly ICloudStorage _storageClient;
        private readonly IDocumentDb _documentDB;

        public SmsController(ILogger<SmsController> logger, ICloudStorage storageClient, IConfiguration configuration, IDocumentDb firestore)
        {
            _logger = logger;
            _storageClient = storageClient;
            _documentDB = firestore;
            
        }

        [HttpPost("/api/sms")]
        public async Task<TwiMLResult> SmsWebhook(SmsRequest request, int numMedia)
        {
            var filenames = await SaveMedia(numMedia);

            await request.AddMessageAsync(_documentDB.ToTenant, filenames);

            var response = new MessagingResponse();
            var messageBody = numMedia == 0 ? "Please send an image!" :
                $"Thanks for sending us { (numMedia > 1 ? $"{numMedia} images!" : "your image!")}\n Review your account and images at ...";

            response.Message(messageBody);
            return TwiML(response);
        }



        private async Task<IEnumerable<string>> SaveMedia(int numMedia)
        {
            var filenames = new List<string>();
            for (var i = 0; i < numMedia; i++)
            {
                var mediaUrl = Request.Form[$"MediaUrl{i}"];
                _logger.LogInformation(mediaUrl);
                var contentType = Request.Form[$"MediaContentType{i}"];

                var filePath = Path.Combine("images", GetMediaFileName(mediaUrl, contentType));
                filenames.Add(filePath);
                await _storageClient.UploadToStorageAsync(mediaUrl, filePath, contentType);
            }

            return filenames;
        }

        private string GetMediaFileName(string mediaUrl,
            string contentType)
        {
            return Path.GetFileName(mediaUrl) + MimeTypeMap.GetExtension(contentType);
        }
    }
}