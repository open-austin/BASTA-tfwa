using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Properties
{
    public record CreatePropertyInput(

         string Name,
         string Street,
         string City,
         string State,
         string PostalCode
            );
}
