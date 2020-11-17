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
                UnitIdentifier = input.UnitIdentifier,
                PropertyId = input.PropertyId
            };
          
            context.Residences.Add(residence);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateResidencePayload(residence);
        }
    }
}
