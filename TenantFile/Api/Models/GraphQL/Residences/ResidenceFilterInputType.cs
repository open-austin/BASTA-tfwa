using HotChocolate.Data.Filters;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Residences
{
    public class ResidenceFilterInputType : FilterInputType<Residence>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Residence> descriptor)
        {
            descriptor.Ignore(t => t.Id);
            descriptor.Ignore(t => t.PropertyId); // todo : fix nullability issue with the descriptor.
        }
    }
}