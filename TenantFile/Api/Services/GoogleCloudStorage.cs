using System.Net;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;

namespace TenantFile.Api.Services
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly GoogleCredential _googleCredential;
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public GoogleCloudStorage(IConfiguration configuration)
        {
            _googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
            _storageClient = StorageClient.Create(_googleCredential);
            _bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
        }

        public async Task UploadToStorageAsync(string fileUrl, string filePath, string contentType)
        {
            WebClient webClient = new WebClient();
            var stream = webClient.OpenRead(fileUrl);

            await _storageClient.UploadObjectAsync(_bucketName, filePath, contentType, stream);
        }
    }
}