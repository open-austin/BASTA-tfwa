using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TenantFile.Api.Models;
using TenantFile.Api.Services;

namespace TenantFile.Api.Controllers
{
    [Authorize]
    public class ImageController : ControllerBase
    {
        // private static readonly string projectId = "tenant-file-fc6de";

        private readonly ILogger<ImageController> _logger;
        private readonly StorageClient _storageClient;
        private readonly IDocumentDb _documentDb;


        public ImageController(ILogger<ImageController> logger, IWebHostEnvironment env, IDocumentDb documentDb)
        {
            _logger = logger;
            _storageClient = StorageClient.Create();
            _documentDb = documentDb;
        }

        [HttpGet("/api/images")]
        public async Task<ActionResult<ImageListResult>> Get()
        {
            var objects = await _storageClient
                        .ListObjectsAsync("tenant-file-fc6de.appspot.com", "images/", new ListObjectsOptions { })
                        .AsAsyncEnumerable().ToListAsync();
            var names = objects.Select(x => x.Name);
            return Ok(new ImageListResult { Images = names });
        }
        
        [HttpGet("/api/images/{Id}")]
        public async Task<ActionResult<ImageListResult>> GetTenantPhotos(string id)
        {
            return Ok(await _documentDb.GetImagesById(id));
        }
       
        [HttpGet("/api/image")]
        public IActionResult GetImage([FromQuery] string name)
        {
            var stream = new MemoryStream();
            try
            {
                stream.Position = 0;
                var obj = _storageClient.GetObject("tenant-file-fc6de.appspot.com", name);
                _storageClient.DownloadObject(obj, stream);
                stream.Position = 0;

                var response = File(stream, obj.ContentType, "file.png"); // FileStreamResult
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
