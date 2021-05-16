using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TenantFile.Api.Services
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public GoogleCloudStorage(IConfiguration configuration, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                var credential = GoogleCredential.GetApplicationDefault();
                _storageClient = StorageClient.Create(credential);
            }
            else
            {
                _storageClient = StorageClient.Create();
            }
            _bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
        }

        public async Task UploadToStorageAsync(string fileUrl, string filePath, string contentType)
        {
            WebClient webClient = new WebClient();
            var stream = webClient.OpenRead(fileUrl);

            await _storageClient.UploadObjectAsync(_bucketName, filePath, contentType, stream);
        }

        public async Task UploadStreamToStorageAsync(Stream stream, string filePath, string contentType)
        {
            await _storageClient.UploadObjectAsync(_bucketName, filePath, contentType, stream);
        }
    }
}