using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Common;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Phones
{
    public class CreatePhonePayload : TPayload<Phone>
    {
        public CreatePhonePayload(Phone phone) : base(phone)
        {

        }
        public CreatePhonePayload(UserError error)
            : base(new[] { error })
        {
        }
    }

}
