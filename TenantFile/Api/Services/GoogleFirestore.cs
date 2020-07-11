using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TenantFile.Api.Models;
using Twilio.AspNet.Common;

namespace TenantFile.Api.Services
{
    public class GoogleFirestore : IDocumentDb
    {
        public Func<SmsRequest, Task<IAsyncEnumerable<object>>> ToTenant { get; }

        private readonly FirestoreDb _db;

        public CollectionReference TenantCollection { get; }

        public GoogleFirestore(IConfiguration configuration, IWebHostEnvironment env)
        {
            _db = FirestoreDb.Create(configuration.GetValue<string>("GoogleProjectId"));
            if (env.IsDevelopment())
            {
                var googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));

            }
            TenantCollection = _db.Collection("accounts");


            ToTenant = async sms =>
            {
                var query = TenantCollection.WhereEqualTo("PrimaryPhone", sms.From);
                var querySnapshot = await query.GetSnapshotAsync();

                IAsyncEnumerable<object>? documentSnap;
                if (querySnapshot.Count == 0)
                {
                    var newTenant = await TenantCollection
                               .AddAsync(new Tenant()
                               {
                                   PrimaryPhone = sms.From,
                                   FamilyName = new string[] { "" },
                                   GivenName = new string[] { "" }
                               });
                    documentSnap = newTenant.GetSnapshotAsync().ToAsyncEnumerable();
                
                }
                else
                {
                    documentSnap = querySnapshot.Documents.ToAsyncEnumerable();
                }
                return documentSnap;
            }; ;
        }


        //public async Task AddMessage(Func<SmsRequest, Task<DocumentSnapshot>> del, Timestamp timestamp, IEnumerable<string> filenames)
        //{
        //    SmsRequest sms = (SmsRequest)del.Method.GetParameters().GetValue(0)!;

        //    await del.Invoke(sms).GetAwaiter().GetResult().Reference.Collection($"Messages")
        //              .Document(timestamp.ToString())
        //              .SetAsync(new TextMessage()
        //              {
        //                  BodyText = sms?.Body,
        //                  TimeReceived = timestamp,
        //                  SentFrom = sms?.From,
        //                  Images = filenames.ToArray()
        //              });
        //}


        public async Task<object> GetTenantReference(SmsRequest request)
        {
            //var startTimestamp = Timestamp.GetCurrentTimestamp();
            var query = TenantCollection.WhereEqualTo("PrimaryPhone", request.From);
            var querySnapshot = await query.GetSnapshotAsync();

            DocumentSnapshot? document;
            if (querySnapshot.Count == 0)
            {
                await TenantCollection
                        .AddAsync(new Tenant()
                        {
                            PrimaryPhone = request.From,
                            FamilyName = new string[] { "" },
                            GivenName = new string[] { "" }
                        });
                var snapshot = await TenantCollection.GetSnapshotAsync();
                document = snapshot.Documents[0];
            }
            else
            {
                document = querySnapshot.Documents[0];
            }
            return document;
        }

        public async Task<ImageListResult> GetImagesById(string id)
        {
            var tenantSnapshot = await TenantCollection.Document(id).GetSnapshotAsync();
            var tenant = tenantSnapshot.ConvertTo<Tenant>();
            return (new ImageListResult { Images = tenant.Images });
        }


        //public async Task AddMessageToTenant(DocumentSnapshot tenantDocument, SmsRequest request, Timestamp timestamp, IEnumerable<string> filenames)
        //{
        //    await tenantDocument.Reference.Collection($"Messages")
        //              .Document(timestamp.ToString())
        //              .SetAsync(new TextMessage()
        //              {
        //                  BodyText = request.Body??"",
        //                  TimeReceived = timestamp,
        //                  SentFrom = request.From,
        //                  Images = filenames.ToArray()
        //              });
        //} 


    }
}