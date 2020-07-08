using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Twilio.AspNet.Common;

namespace TenantFile.Api.Services
{
    public interface IDocumentDb
    {
        public Func<SmsRequest, Task<DocumentSnapshot>> ToTenant { get; }
        public CollectionReference TenantCollection { get; }
        public Task AddMessageToTenant(DocumentSnapshot document, SmsRequest request, Timestamp timestamp, IEnumerable<string> filenames);
        public Task<DocumentSnapshot> GetTenantReference(SmsRequest request);

    }
}