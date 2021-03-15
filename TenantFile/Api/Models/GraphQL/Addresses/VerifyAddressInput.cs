using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models.Addresses
{
    public record VerifyAddressInput
    (
         string? Line1,
         string? Line2,
         string? Line3,
         string? Line4,
         string? City,
         string? State,
         string? PostalCode
    );
}
