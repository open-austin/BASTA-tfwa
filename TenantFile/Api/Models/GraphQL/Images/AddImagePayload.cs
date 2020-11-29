using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Common;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Images
{
    public class AddImagePayload : TPayload<Image>
    {
        public AddImagePayload(Image image) : base(image)
        {
        }

        public AddImagePayload(UserError errors) : base(new[] { errors })
        {
        }
    }
}
