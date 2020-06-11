using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    public class Organizer : User
    {
        public Organizer(string givenName, string familyName, string phoneNumber, string language) : base(givenName, familyName, phoneNumber, language)
        {
        }
    }
}
