using TenantFile.Api.Models.Addresses;

namespace TenantFile.Api.Models.Residences
{
    public record CreateResidenceInput
    (
         CreateAddressInput AddressInput,
         int? PropertyId
    );
}
