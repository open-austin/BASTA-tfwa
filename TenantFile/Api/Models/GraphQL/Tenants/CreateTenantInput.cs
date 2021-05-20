using HotChocolate.Types.Relay;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Models.Residences;

namespace TenantFile.Api.Models.Tenants
{
    public record CreateTenantInput(
        string Name,
        string PhoneNumber,
        CreateResidenceInput? CurrentResidence,
        [ID(nameof(Residence))] int? ResidenceId
        )
    { }
}
