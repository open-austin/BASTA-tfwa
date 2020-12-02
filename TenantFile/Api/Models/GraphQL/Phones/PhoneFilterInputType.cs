using HotChocolate.Data.Filters;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Phones
{
    public class PhoneFilterInputType : FilterInputType<Phone>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Phone> descriptor)
        {
            descriptor.Ignore(t => t.Id);
        }
    }
}