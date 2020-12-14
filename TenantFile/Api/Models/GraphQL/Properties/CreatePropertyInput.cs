using TenantFile.Api.Models.Addresses;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Properties
{
    public record CreatePropertyInput(
         CreateAddressInput AddressInput,
         string Name
       );
}
