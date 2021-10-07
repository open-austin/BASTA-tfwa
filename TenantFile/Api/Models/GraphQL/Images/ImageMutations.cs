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
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class ImageMutations
    {/// <summary>
     /// Intended for use by Organizer uploading Images via dashboard, not for SMS/Twilio uploads
     /// The relationshiup of the image and the residence is implied as of 2/11 would need to add a Property on the image or Residence Entity
     /// </summary>
     /// <param name="input"></param>
     /// <param name="context"></param>
     /// <returns></returns>
        [UseTenantFileContext]

        public async Task<AddImagePayload> AddImageAsync(
        AddImageInput input,
        [ScopedService] TenantFileContext context)
        {
            var (fileName, thumb, tenantId, residenceId, labels) = input;

            var image = new Image
            {
                Name = fileName,
                Labels = labels,
                ThumbnailName = thumb,

            };
            context.Images.Add(image);
            
            var phones
             = context.Phones.AsQueryable()
                .Where(p => p.Tenants.Select(t => t.Id).Contains(tenantId)).ToList();
                phones.ForEach(p => p.Images.Add(image));




            await context.SaveChangesAsync();
            return new AddImagePayload(image);
        }
    }
}
