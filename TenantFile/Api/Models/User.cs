using Google.Apis.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    public class User
    {
        public string AccountId { get; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Language { get; set; }

        protected User(string givenName, string familyName, string phoneNumber, string language, string? accountId = null)
        {
            AccountId = accountId ?? Guid.NewGuid().ToString();
            GivenName = givenName;
            FamilyName = familyName;
            PhoneNumber = phoneNumber;
            Language = language;
        }
    }
}
