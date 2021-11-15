using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Common;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Tenants
{
    public class EditTenantPayload : TPayload<Tenant>
    {
        public EditTenantPayload(Tenant tenant) : base(tenant)
        {

        }
        public EditTenantPayload(UserError error)
            : base(new[] { error })
        {
            Console.WriteLine($"Error when editing Tenant... message: {error.Message} code: {error.Code}");
        }
    }
}
