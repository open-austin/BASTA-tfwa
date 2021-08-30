using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TenantFile.Api.Common;
using TenantFile.Api.Models;

namespace TenantFile.Api.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;
        private readonly StorageClient _storageClient;
        private readonly TenantFileContext _context;
        private const string bucket = "tenant-file-fc6de.appspot.com";
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

        [AllowAnonymous]
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

                FileStreamResult response = File(stream, obj.ContentType, $"{name}"); // FileStreamResult
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestResult();
            }
        }
        [AllowAnonymous]
        [HttpPost("/api/batchImage")]
        public async Task<IActionResult> GetBatchImagesAsync()
        {
            JsonSerializerOptions options = new(JsonSerializerDefaults.Web)
            {
                WriteIndented = true
            };
            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<BatchImageData[]>(requestBody);

            //  var request = await JsonSerializer.DeserializeAsync<BatchImageData[]>(requestBody, options);
            // var images = request!["images"].EnumerateArray().ToList();

            try
            {
                using var outputStream = new MemoryStream();
                using var archive = new ZipArchive(outputStream, ZipArchiveMode.Create, true);
                //ms.Position = 0;
                // Will likely be usuful to have TenantData and Property data seperate classes
                foreach (var imageData in data!)
                {
                    using var ms = new MemoryStream();
                    var googleObject = await _storageClient.GetObjectAsync(bucket, imageData.imageUrl);

                    await _storageClient.DownloadObjectAsync(googleObject, ms);
                    var imageNameWithExtension = imageData!.imageUrl!.Split("/")[1];
                    //CreateEntry needs a full file name with extension. Can have a directory structure.
                    var zipArchiveEntry = archive.CreateEntry($"{imageData!.tenantName}/{imageNameWithExtension.Split(".")[0]}/{imageNameWithExtension}", CompressionLevel.Optimal);
                    using (var zipStream = zipArchiveEntry.Open())
                    {

                        //await outputStream.CopyToAsync(zipStream);
                        zipStream.Write(ms.ToArray());
                        //zipStream.Flush();
                    }

                    var zipArchiveEntry2 = archive.CreateEntry($"{imageData!.tenantName}/{imageNameWithExtension.Split(".")[0]}/{imageNameWithExtension}-labels.txt", CompressionLevel.Optimal);
                    using (var zipStream2 = zipArchiveEntry2.Open())
                    {

                        zipStream2.Write(Encoding.ASCII.GetBytes(string.Concat(imageData!.labels!.Select(l => l.Label + "\r").ToList())));

                    }

                }

                archive.Dispose();

                outputStream.Seek(0, SeekOrigin.Begin);

                //var response = File(outputStream.ToArray(), "application/zip", "Images.zip");
                var packageId = Guid.NewGuid().ToString();
                var uploadResult = await _storageClient.UploadObjectAsync(bucket, "packages/" + packageId + ".zip", MediaTypeNames.Application.Zip, outputStream);
                //return response;
                return Json(new { id = packageId });
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestResult();
            }
        }



        [AllowAnonymous]
        [HttpGet("/api/getPackage")]
        public async Task<IActionResult> GetPackageAsync([FromQuery] string packageID)
        {
            MemoryStream ms = new MemoryStream();

            var googleObject = await _storageClient.GetObjectAsync(bucket, packageID);
            //googleObject.ContentDisposition
            await _storageClient.DownloadObjectAsync(googleObject, ms);

            FileStreamResult response = File(ms, "application/zip", "topic.zip");
            return response;
        }

        [AllowAnonymous]
        [HttpGet("/api/batchImage")]
        public async Task<IActionResult> GetBatchImagesAsync([FromQuery] string names)
        {
            //Return a folder? of images requested
            //Compile all names of images in a url
            //    generate a cookie that can pass the auth in new //endpoint 
            //    hit the entry and ext endpoint on navigation from //tenant profile. entry creates cookie, exit //invalidates and deletes it.
            //    may need a redirect


            //using var stream = new MemoryStream();
            try
            {
                string[] imageList = names.Split('^');
                // stream.Position = 0;
                // var obj = _storageClient.GetObject("tenant-file-fc6de.appspot.com", "images/" + imageList[0]);
                // await _storageClient.DownloadObjectAsync(obj, stream);
                // stream.Position = 0;

                //var response = File(stream, obj.ContentType, $"{name}"); // FileStreamResult
                //using var zip = new ZipArchive(stream, ZipArchiveMode.Update);
                //ZipArchiveEntry readmeEntry = zip.CreateEntry(imageList[0]);



                //return File(zip);


                MemoryStream ms = new MemoryStream();

                ms.Position = 0;
                using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                    // {
                    foreach (var name in imageList)
                    {

                        System.Console.WriteLine($"name: {name}");
                        var googleObject = await _storageClient.GetObjectAsync("tenant-file-fc6de.appspot.com", name);
                        await _storageClient.DownloadObjectAsync(googleObject, ms);

                        var zipArchiveEntry = archive.CreateEntry(name, CompressionLevel.Optimal);
                        using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(ms.GetBuffer().ToArray());


                    }
                // var zipArchiveEntry = archive.CreateEntry("file1.txt", CompressionLevel.Fastest);
                // using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(file1, 0, file1.Length);

                // zipArchiveEntry = archive.CreateEntry("file2.txt", CompressionLevel.Fastest);
                // using (var zipStream = zipArchiveEntry.Open()) zipStream.Write(file2, 0, file2.Length);
                ms.Position = 0;
                var response = File(ms, "application/zip", "Images.zip");
                return response;

                //}

                //return File(ms.ToArray(), "application/zip", "Archive.zip");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestResult();
            }
        }
    }
}
