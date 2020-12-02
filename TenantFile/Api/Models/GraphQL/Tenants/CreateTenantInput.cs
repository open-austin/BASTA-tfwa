using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Tenants
{
    public record CreateTenantInput(
        string Name,
        string PhoneNumber,
        Residence CurrentResidence
        );
}
