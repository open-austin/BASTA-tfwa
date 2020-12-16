using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Images
{
    public record AddImageInput(
        string FileName,
        Tenant Tenant,
        Residence Residence,
        ImageLabel[] Labels

        );
  
}
