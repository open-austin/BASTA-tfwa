using HotChocolate.Types.Relay;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Images
{
    public record AddImageInput(
        string FileName,
        string ThumbnailName,
        [ID(nameof(Tenant))]
        int Tenant,
        [ID(nameof(Residence))]
        int Residence,
        ImageLabel[] Labels

        );

}
