using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using TenantFile.Api.Models;

namespace TenantFile.Api.Extensions
{
    public class UseTenantFileContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
        {
            descriptor.UseDbContext<TenantFileContext>();
        }
    }
}