using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Common;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Properties
{
    public class CreatePropertyPayload : TPayload<Property>
    {
        public CreatePropertyPayload(Property property) : base(property)
        {

        }
        public CreatePropertyPayload(UserError error)
            : base(new[] { error })
        {
        }
    }
}