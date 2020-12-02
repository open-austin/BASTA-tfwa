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
                .ResolveNode((ctx, id) => ctx.DataLoader<DataLoaderById<Residence>>().LoadAsync(id, ctx.RequestAborted));

            descriptor.Field(r => r.Address)
                      .ResolveWith<ResidenceResolvers>(r => r.GetAddressAsync(default!, default!, default!))
                      .UseTenantContext<TenantFileContext>()
                      .Name("address");
            descriptor.Field(r => r.Property)
                      .ResolveWith<ResidenceResolvers>(r => r.GetPropertyAsync(default!, default!, default!))
                      .UseTenantContext<TenantFileContext>()
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
           DataLoaderById<Address> dataLoader,
           //[ScopedService] TenantFileContext context,
           CancellationToken cancellationToken)
        {
            //var addressId = context.Residences.AsQueryable()//don't use include...add AddresssId to Entity...Address is the Princpal for Property, Residence and Complex
            //   .Where(p => p.Id == residence.Id)
            //   .Select(r => r.AddressId)
            //   .SingleOrDefault();//Could return Address here BUT I believe fetching the ID then passing them all to the Dataloader to make one call to the DB is the benefit ofthe dataloader? n+1?


            return await dataLoader.LoadAsync(residence.AddressId, cancellationToken);

        }
        public async Task<Property?> GetPropertyAsync(
           Residence residence,
           //[ScopedService] TenantFileContext context,
           DataLoaderById<Property> dataLoader,
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