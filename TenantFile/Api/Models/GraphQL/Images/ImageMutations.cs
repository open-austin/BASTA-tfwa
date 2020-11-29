using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Images
{
    [ExtendObjectType(Name = "Mutation")]
    public class ImageMutations
    {
        [UseTenantFileContext]
        public async Task<AddImagePayload> AddImageAsync(
        AddImageInput input,
        [ScopedService] TenantFileContext context)

        {
            var image = new Image{ Name = input.FileName };
            context.Images.Add(image);
            await context.SaveChangesAsync();
            return new AddImagePayload(image);
        }
    }
}
