﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Common;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Tenants
{
    public class UpdateTenantPayload : TPayload<Tenant>
    {
        public UpdateTenantPayload(Tenant tenant) : base(tenant)
        {
           
        }
        public UpdateTenantPayload(UserError error)
            : base(new[] { error })
        {
        }
    }
}
