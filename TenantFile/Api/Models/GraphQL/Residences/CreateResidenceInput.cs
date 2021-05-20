using HotChocolate.Types.Relay;
using TenantFile.Api.Models.Addresses;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Residences
{
    public record CreateResidenceInput
    (
         CreateAddressInput AddressInput,
         [ID(nameof(Property))]int? PropertyId
    );
}
