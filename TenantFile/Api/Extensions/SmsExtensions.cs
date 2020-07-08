using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Models;
using TenantFile.Api.Services;
using Twilio.AspNet.Common;

namespace TenantFile.Api.Extentions
{
    public static class SmsExtensions
    {

        public static async Task AddMessageAsync<T>(this T message,
            Func<T, Task<DocumentSnapshot>> tenantDelegate, IEnumerable<string> filenames) where T : SmsRequest
        {
            Timestamp timestamp = Timestamp.GetCurrentTimestamp();

            //Tenant created if non-existant through delegate method, reference returned
            DocumentSnapshot snap = await tenantDelegate.Invoke(message);

            //Update Image array on Tenant
            await snap.Reference.UpdateAsync("Images",
                                    FieldValue.ArrayUnion(filenames.ToArray()[..]));

            //Add new TextMessage to Message Collection
            await snap.Reference.Collection($"Messages")
               .Document(timestamp.ToString())
               .SetAsync(new TextMessage()
               {
                   BodyText = message.Body ?? string.Empty,
                   TimeReceived = timestamp,
                   SentFrom = message.From,
                   Images = filenames.ToArray()
               });
        }

    }
}
