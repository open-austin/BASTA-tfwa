using System;
using Xunit;
using TenantFile.Api.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using TenantFile.Api.Services;
using TenantFile.Api.Models;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using Twilio.AspNet.Common;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Api.UnitTests
{
    public class SmsControllerTests
    {
        // [Fact]
        // public async Task UploadsImageAndReturnsCorrectResponse()
        // {
        //     // ARRANGE
        //     var cloudStorageMock = new Mock<ICloudStorage>();
        //     cloudStorageMock.Setup(client => client.UploadToStorageAsync("", "", ""))
        //         .Returns(Task.FromResult(0));

        //     var collection = Mock.Of<IFormCollection>();
        //     var request = new Mock<HttpRequest>();
        //     request.Setup(f => f.ReadFormAsync(CancellationToken.None)).Returns
        //                         (Task.FromResult(collection));

        //     // Set up form details

        //     var formData = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
        //     {
        //         { "MediaUrl0", "http://test.test/image" },
        //         { "MediaContentType0", "image/png" }
        //     });

        //     var requestMock = new Mock<HttpRequest>();
        //     requestMock.SetupGet(x => x.Form).Returns(formData);

        //     var contextMock = new Mock<HttpContext>();
        //     contextMock.SetupGet(x => x.Request).Returns(requestMock.Object);

        //     var ctx = new ControllerContext
        //     {
        //         HttpContext = contextMock.Object
        //     };

        //     var myConfiguration = new Dictionary<string, string>
        //     {
        //         {"GoogleProjectId", "tenant-file-fc6de"},
        //     };

        //     var configuration = new ConfigurationBuilder()
        //         .AddInMemoryCollection(myConfiguration)
        //         .Build();

        //     var mockContext = new Mock<TenantContext>(configuration);

        //     var controller = new SmsController(Mock.Of<ILogger<SmsController>>(), cloudStorageMock.Object, configuration, mockContext.Object)
        //     {
        //         ControllerContext = ctx
        //     };

        //     var smsRequest = new SmsRequest
        //     {
        //         From = "(555) 555-1234",
        //         Body = "TEST"
        //     };

        //     // ACT
        //     await controller.SmsWebhook(smsRequest, 1);

        //     // ASSERT
        //     cloudStorageMock.Verify(mock => mock.UploadToStorageAsync("http://test.test/image", "images/image.png", "image/png"));
        // }
    }
}
