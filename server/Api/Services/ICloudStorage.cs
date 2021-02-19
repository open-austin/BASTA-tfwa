using System.IO;
using System.Threading.Tasks;

namespace TenantFile.Api.Services
{
    public interface ICloudStorage
    {
        Task UploadToStorageAsync(string fileUrl, string filePath, string contentType);
        Task UploadStreamToStorageAsync(Stream stream, string filePath, string contentType);
    }
}