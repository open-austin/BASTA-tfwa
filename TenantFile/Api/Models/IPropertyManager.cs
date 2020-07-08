using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    public interface IPropertyManager
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Type EntityType { get; }

    }
}
