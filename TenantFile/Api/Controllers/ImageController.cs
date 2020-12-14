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

namespace TenantFile.Api.Controllers
{
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly StorageClient _storageClient;
        private readonly TenantFileContext _context;

        public ImageController(ILogger<ImageController> logger, TenantFileContext context)
        {
            _logger = logger;
            _storageClient = StorageClient.Create();
            _context = context;
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
				_logger.LogError(ex.Message);
                return new BadRequestResult();
            }
        }
    }
}
