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
    public class TextMessage
    {
        [FirestoreProperty]
        public string[]? Images { get; set; }

        [FirestoreProperty]
        public string? SentFrom { get; set; }

        [FirestoreProperty]
        public string? BodyText { get; set; }

        [FirestoreProperty]
        public Timestamp? TimeReceived { get; set; }



    }
}

