using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models.Phones
{
    public class PhoneType : ObjectType<Phone>
    {

        protected override void Configure(IObjectTypeDescriptor<Phone> descriptor)
        {
          
        }
    }
}
