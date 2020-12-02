using HotChocolate.Data.Filters;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Properties
{
    public class PropertyFilterInputType : FilterInputType<Property>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Property> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}