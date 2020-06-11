using Google.Apis.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    public class Tenant : User
    {
       
        List<Property?> ResidenceHistory { get; set; } = new List<Property?>();

        public Tenant(string givenName, string familyName, string phoneNumber, string language, Property? residence = null) : base(givenName, familyName, phoneNumber, language)
        {
            if (residence != null)
            {
                ResidenceHistory.Add(residence);
            }

        }
    }
}
