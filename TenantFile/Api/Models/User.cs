using Google.Apis.Http;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreDocumentId]
        public string? AccountId { get; set; }

        [FirestoreProperty]
        public string[]? GivenName { get; set; }

        [FirestoreProperty]
        public string[]? FamilyName { get; set; }

        [FirestoreProperty]
        public string? PrimaryPhone { get; set; }

        [FirestoreProperty]
        public string? Language { get; set; }

        public User()
        {

        }
        // protected User(string givenName, string familyName, string phoneNumber, string language, string? accountId = null)
        // {
        //     AccountId = accountId ?? Guid.NewGuid().ToString();
        //     GivenName = givenName;
        //     FamilyName = familyName;
        //     PhoneNumber = phoneNumber;
        //     Language = language;
        // }
    }
}
