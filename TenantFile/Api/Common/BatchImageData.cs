using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Common
{
    public class BatchImageData{

      public string? imageUrl { get; set; }
        public string? tenantName { get; set; }
      public string? phoneNumber { get; set; }
      public  ImageLabel[]? labels { get; set; }
    }
    
}