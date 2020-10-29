using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Common;

namespace TenantFile.Api.Models.Tenants
{
    public class CreateTenantPayload : TPayload<Tenant>
    {
        public CreateTenantPayload(Tenant tenant) : base(tenant)
        {
           
        }
        public CreateTenantPayload(UserError error)
            : base(new[] { error })
        {
        }
    }
}
