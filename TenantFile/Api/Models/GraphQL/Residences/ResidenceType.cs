using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Residences
{
    public class ResidenceType : ObjectType<Residence>
    {

        protected override void Configure(IObjectTypeDescriptor<Residence> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<ResidenceByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor.Field(r => r.Address)
                      .UseTenantContext<TenantFileContext>()
                      .ResolveWith<ResidenceResolvers>(r => r.GetAddressAsync(default!, default!, default!))
                      .Name("address");
            descriptor.Field(r => r.Property)
                      .UseTenantContext<TenantFileContext>()
                      .ResolveWith<ResidenceResolvers>(r => r.GetPropertyAsync(default!, default!, default!))
                      .Name("property");
        }
    }

    public class ResidenceResolvers
    {
        public IQueryable<Residence> GetResidence(
           Residence residence,
           [ScopedService] TenantFileContext context)
        {
            return context.Residences.AsQueryable()
                 .Where(r => r.Id == residence.Id);
        }
        public async Task<Address> GetAddressAsync(
           Residence residence,
           AddressByIdDataLoader dataLoader,
           CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync(residence.AddressId, cancellationToken);
        }
        public async Task<Property?> GetPropertyAsync(
           Residence residence,
           PropertyByIdDataLoader dataLoader,
           CancellationToken cancellationToken)
        {                     
            if (residence.PropertyId == null)
            {
                return null;
            }

            return await dataLoader.LoadAsync(residence.PropertyId.Value, cancellationToken).ConfigureAwait(false);
        }
    }
}