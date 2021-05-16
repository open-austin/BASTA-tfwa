using HotChocolate.Types.Relay;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Models.Residences;

namespace TenantFile.Api.Models.Tenants
{
    public record UpdateTenantInput(
        [ID(nameof(Tenant))] int TenantId,
        string? Name,
        string? PhoneNumber,
        [ID(nameof(Residence))] int? ResidenceId //this can be handled by the Residence Input if reconfigured
        ){}
}
