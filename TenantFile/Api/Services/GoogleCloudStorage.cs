using System;
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
                var googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
                _storageClient = StorageClient.Create(googleCredential);
            }
            else
            {
                _storageClient = StorageClient.Create();
            }
            _bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
        }

        public async Task UploadToStorageAsync(string fileUrl, string filePath, string contentType)
        {
            // Upload image to firebase
            WebClient webClient = new WebClient();
            var stream = webClient.OpenRead(fileUrl);

            await _storageClient.UploadObjectAsync(_bucketName, filePath, contentType, stream);

            // Reshape the image to 128x128
            // TODO Hannah how do I import??
            Image originalImage = Image.FromStream(stream);
            Image thumbNail = ImageResize.Crop(originalImage, 128, 128, ImageResize.AnchorPosition.Top);

            // Uplaod the image to firebase
            // TODO Hannah can I just put an image directly into OpenRead?
            var thumbnailStream = webClient.OpenRead(thumbNail);
            var thumbNailFilePath = "thumbnail/" + filePath;
            await _storageClient.UploadObjectAsync(_bucketName, thumbNailFilePath, contentType, thumbnailStream);
        }
    }
}