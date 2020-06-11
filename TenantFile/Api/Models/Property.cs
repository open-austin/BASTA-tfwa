using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    public class Property 
    {
        public IPropertyManager? Management { get; set; }
        public Address Location { get; set; }

        public Property(Address location,
                         IPropertyManager? mangament
                        ) 
        {
            Location = location;
            Management = mangament;
        }



}
}
