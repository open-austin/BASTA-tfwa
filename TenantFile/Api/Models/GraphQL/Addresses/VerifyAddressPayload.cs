using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TenantFile.Api.Common;
using TenantFile.Api.Models.Entities;
using TenantFile.Api.Services;

namespace TenantFile.Api.Models.Addresses
{
    public class VerifyAddressPayload : TPayload<USPSVerifyPayload>
    {
       
        public VerifyAddressPayload(USPSVerifyPayload address) : base(address)
        {

        }
        public VerifyAddressPayload(UserError error)
            : base(new[] { error })
        {
        }
    }
}