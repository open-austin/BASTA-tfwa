using HotChocolate.Data.Filters;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Addresses
{
    internal class AddressFilterInputType : FilterInputType<Address>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Address> descriptor)
        {
            descriptor.Ignore(t => t.Id);
           
        }
    }
}
