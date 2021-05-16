using HotChocolate.Types.Relay;
using TenantFile.Api.Models.Addresses;

namespace TenantFile.Api.Models.Residences
{
    public record CreateResidenceInput
    (
         CreateAddressInput AddressInput,
         [ID(nameof(Property))]int? PropertyId
    );
}
