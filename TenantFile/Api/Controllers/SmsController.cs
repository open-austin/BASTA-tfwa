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

namespace TenantFile.Api.Controllers
{
    public class SmsController : TwilioController
    {
        private readonly ILogger<SmsController> _logger;
        private readonly ICloudStorage _storageClient;
        private readonly FirestoreDb _db;

        public SmsController(ILogger<SmsController> logger, ICloudStorage storageClient, IConfiguration configuration)
        {
            _logger = logger;
            _storageClient = storageClient;

            _db = FirestoreDb.Create(configuration.GetValue<string>("GoogleProjectId"));
        }

        [HttpPost("/api/sms")]
        public async Task<TwiMLResult> SmsWebhook(SmsRequest request, int numMedia)
        {
            // Is this number in our database?

            var accountsRef = _db.Collection("accounts");
            var query = accountsRef.WhereEqualTo("PhoneNumber", request.From);
            var querySnapshot = await query.GetSnapshotAsync();
            DocumentSnapshot? document;
            if (querySnapshot.Count == 0)
            {
                await accountsRef.Document(Guid.NewGuid().ToString())
                        .SetAsync(new Dictionary<string, object>(){
                            { "PhoneNumber", request.From }
                        });
                var snapshot = await query.GetSnapshotAsync();
                document = snapshot.Documents[0];
            }
            else
            {
                document = querySnapshot.Documents[0];
            }

            // Save the message body if there is one
            if (request.Body != null)
            {
                await document.Reference.UpdateAsync("Messages", FieldValue.ArrayUnion(new Dictionary<string, object>()
                {
                    {"Text", request.Body},
                    {"Timestamp", Timestamp.GetCurrentTimestamp()}
                }));
            }


            var filenames = await SaveMedia(numMedia);

            var response = new MessagingResponse();
            var messageBody = numMedia == 0 ? "Send us an image!" :
                $"Thanks for sending us {numMedia} file(s)!";
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