using HotChocolate.Data.Filters;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Images
{
    internal class ImageFilterInputType : FilterInputType<Image>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Image> descriptor)
        {
            descriptor.Ignore(t => t.Id);
            
        }
    }
}