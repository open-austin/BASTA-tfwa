using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Residences
{
    [ExtendObjectType(Name = "Mutation")]
    public class ResidenceMutations
    {
        [UseTenantFileContext]
        public async Task<CreateResidencePayload> CreateResidence(CreateResidenceInput input,
                [ScopedService] TenantFileContext context, CancellationToken cancellationToken)

        {
            var residence = new Residence
            {
                PropertyId = input.PropertyId,

                Address = new Address()
                {
                    City = input.AddressInput.City,
                    PostalCode = input.AddressInput.PostalCode,
                    State = input.AddressInput.State,
                    Line1 = input.AddressInput.Line1,
                    Line2 = input.AddressInput.Line2,
                    Line3 = input.AddressInput.Line3,
                    Line4 = input.AddressInput.Line4
                }
            };

            context.Residences.Add(residence);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateResidencePayload(residence);
        }
    }
}
