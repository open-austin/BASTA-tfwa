using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using TenantFile.Api.Models;
using Twilio.AspNet.Common;

namespace TenantFile.Api.Services
{
    public interface IDocumentDb
    {
        public Func<SmsRequest, Task<IAsyncEnumerable<object>>> ToTenant { get; }
        public Task<object> GetTenantReference(SmsRequest request);
        public Task<ImageListResult> GetImagesById(string Id);

    }
}