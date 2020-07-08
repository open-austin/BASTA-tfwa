using System;
using Xunit;
using TenantFile.Api.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using TenantFile.Api.Services;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using Twilio.AspNet.Common;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Google.Cloud.Firestore;
using Google.Api.Gax;
using Google.Cloud.Firestore.V1;
using TenantFile.Api.Models;
using TenantFile.Api.Extentions;
using System.Linq;

namespace Api.UnitTests
{
    public class SmsControllerTests : IDisposable
    {               
        Mock<ICloudStorage> cloudStorageMock;
        Mock<IDocumentDb> firestoreMock;
        Timestamp startTimestamp;
        FirestoreDbBuilder tree;
        FirestoreDb emulatedFirestore;
        private bool disposedValue;

        public SmsControllerTests()
        {

            startTimestamp = Timestamp.GetCurrentTimestamp();
            tree = new FirestoreDbBuilder();
            tree.EmulatorDetection = EmulatorDetection.EmulatorOnly;
            tree.ProjectId = "tenant-file-fc6de";

            emulatedFirestore = tree.Build();

            cloudStorageMock = new Mock<ICloudStorage>();
            firestoreMock = new Mock<IDocumentDb>();
        }
        [Fact]
        public async Task UploadsImageAndReturnsCorrectResponse()
        {
            // ARRANGE
            var cloudStorageMock = new Mock<ICloudStorage>();
            cloudStorageMock.Setup(client => client.UploadToStorageAsync("", "", ""))
                .Returns(Task.FromResult(0));

            var collection = Mock.Of<IFormCollection>();
            var request = new Mock<HttpRequest>();
            request.Setup(f => f.ReadFormAsync(CancellationToken.None)).Returns
                                (Task.FromResult(collection));

            // Set up form details

            var formData = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "MediaUrl0", "http://test.test/image" },
                { "MediaContentType0", "image/png" }
            });

            var requestMock = new Mock<HttpRequest>();
            requestMock.SetupGet(x => x.Form).Returns(formData);

            var contextMock = new Mock<HttpContext>();
            contextMock.SetupGet(x => x.Request).Returns(requestMock.Object);

            var ctx = new ControllerContext
            {
                HttpContext = contextMock.Object
            };

            var myConfiguration = new Dictionary<string, string>
            {
                {"GoogleProjectId", "tenant-file-fc6de"},
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var controller = new SmsController(Mock.Of<ILogger<SmsController>>(), cloudStorageMock.Object, configuration, firestoreMock.Object)
            {
                ControllerContext = ctx
            };

            var smsRequest = new SmsRequest
            {
                From = "(555) 555-1234",
                Body = "TEST"
            };

            // ACT
            await controller.SmsWebhook(smsRequest, 1);

            // ASSERT
            cloudStorageMock.Verify(mock => mock.UploadToStorageAsync("http://test.test/image", "images/image.png", "image/png"));
        }
        [Fact]
        public async Task TestModels()
        {
            SmsRequest smsRequestOne = new SmsRequest
            {
                From = "(555) 555-1234",
                Body = "TEST One"
            };
            IEnumerable<string> filenamesOne = new List<string>()
            { "filename1.png", "filename2.png" };
            SmsRequest smsRequestTwo = new SmsRequest
            {
                From = "(555) 555-1234",
                Body = "TEST Two"
            };
            IEnumerable<string> filenamesTwo = new List<string>()
            { "filename3.png", "filename4.png" };
            SmsRequest smsRequestThree = new SmsRequest
            {
                From = "(412) 555-1234",
                Body = "TEST Three"
            };
            IEnumerable<string> filenamesThree = new List<string>()
            { "filename5.png", "filename6.png", "filename7.png" };

            Tenant tenantOne = new Tenant()
            {
                GivenName = new string[] { },
                FamilyName = new string[] { },
                Language = null,
                PrimaryPhone = "(555) 555-1234",
                Images = new List<string>() { "filename1.png", "filename2.png", "filename3.png", "filename4.png" }
            };
            TextMessage messageOne = new TextMessage()
            {
                Images = new string[] { "filename1.png", "filename2.png" },
                SentFrom = "(555) 555-1234",
                BodyText = "TEST One"

            };
            TextMessage messageTwo = new TextMessage()
            {
                Images = new string[] { "filename3.png", "filename4.png" },
                SentFrom = "(555) 555-1234",
                BodyText = "TEST Two"

            };
            Tenant tenantTwo = new Tenant()
            {
                GivenName = new string[] { },
                FamilyName = new string[] { },
                Language = null,
                PrimaryPhone = "(412) 555-1234",
                Images = new List<string>() { "filename5.png", "filename6.png", "filename7.png" }
            };
            TextMessage messageThree = new TextMessage()
            {
                Images = new string[] { "filename5.png", "filename6.png", "filename7.png" },
                SentFrom = "(412) 555-1234",
                BodyText = "TEST Three"

            };


            firestoreMock.SetupGet(db => db.TenantCollection).Returns(emulatedFirestore.Collection("accounts"));

            firestoreMock.SetupGet(db => db.ToTenant).Returns(
             async sms =>
             {
                 var query = firestoreMock.Object.TenantCollection.WhereEqualTo("PrimaryPhone", sms.From);
                 var querySnapshot = await query.GetSnapshotAsync();

                 DocumentSnapshot? documentSnap;
                 if (querySnapshot.Count == 0)
                 {
                     await firestoreMock.Object.TenantCollection
                             .AddAsync(new Tenant()
                             {
                                 PrimaryPhone = sms.From,
                                 FamilyName = new string[] { "" },
                                 GivenName = new string[] { "" }
                             });
                     var snapshot = await firestoreMock.Object.TenantCollection.GetSnapshotAsync();
                     documentSnap = snapshot.Documents[0];
                 }
                 else
                 {
                     documentSnap = querySnapshot.Documents[0];
                 }
                 return documentSnap;
             });

            //Add Messages to the emulated db, if the account already exists, it will be added to the account matching the phone number
            await smsRequestOne.AddMessageAsync(firestoreMock.Object.ToTenant, filenamesOne);
            await smsRequestTwo.AddMessageAsync(firestoreMock.Object.ToTenant, filenamesTwo);
            await smsRequestThree.AddMessageAsync(firestoreMock.Object.ToTenant, filenamesThree);

            //Read our newly written SmsRequests from the emulated db and convert them to POCO TextMessages
            var accountDocRefs = firestoreMock.Object.TenantCollection.ListDocumentsAsync();
            var docSnaps = await accountDocRefs.SelectMany(m => m.Collection("Messages").ListDocumentsAsync()).ToListAsync();
            var ordered = docSnaps.OrderBy(o => o.Id);
            var messagesSnaps = ordered.Select(m => m.GetSnapshotAsync());
            var messagesFromDb = messagesSnaps.Select(m => m.GetAwaiter().GetResult().ConvertTo<TextMessage>()).ToList();

            int i = 0;
            var messages = new TextMessage[] { messageOne, messageTwo, messageThree };

            //Run checks for TextMessage deserialization
            messagesFromDb.ForEach(text =>
            {
                Assert.Equal(messages[i].BodyText, text.BodyText);
                Assert.Equal(messages[i].Images, text.Images);
                Assert.Equal(messages[i].Images[0], text.Images[0]);
                Assert.Equal(messages[i].Images[1], text.Images[1]);
                Assert.Equal(messages[i].SentFrom, text.SentFrom);
                i++;
            });

            //Read our newly written tenants accounts from the emulated db and convert them to POCO TextMessages
            var tenantsSnaps = accountDocRefs.SelectAwait(async a => await a.GetSnapshotAsync());
            var tenantsFromDb = tenantsSnaps.Select(tenant => tenant.ConvertTo<Tenant>());
            var tenants = new List<Tenant>() { tenantTwo, tenantOne }; //reads in order of last written to db so these are switched?
            int ii = 0;
            //Run checks for Tenant deserialization
            await tenantsFromDb.ForEachAsync(tenant =>
            {
                Assert.Equal(tenants[ii].PrimaryPhone, tenant.PrimaryPhone);
                Assert.Equal(tenants[ii].Images.Count, tenant.Images.Count);
                Assert.Equal(tenants[ii].Images, tenant.Images);
                Assert.Equal(tenants[ii].Language, tenant.Language);
                Assert.NotNull(tenant.AccountId);
                ii++;
            });

        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)


                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SmsControllerTests()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
