using HotChocolate.Data.Filters;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Tenants
{
    public class TenantFilterInputType : FilterInputType<Tenant>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Tenant> descriptor)
        {
            descriptor.Ignore(t => t.Id);
       
        }
    }
}