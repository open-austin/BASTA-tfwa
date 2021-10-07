using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.Models;

using File = Google.Apis.Drive.v3.Data.File;

namespace TenantFile.Api.Services
{
    public class GoogleDriveService
    {
        private readonly DriveService _driveService;
        private readonly TenantFileContext _context;
        private readonly WebClient _webClient = new();

        public GoogleDriveService(DriveService driveService, TenantFileContext context)
        {
            _driveService = driveService;

            _context = context;
        }

        public GoogleDriveService(TenantFileContext context)
        {
            var credential = GoogleCredential.GetApplicationDefault().CreateScoped(DriveService.Scope.Drive);
            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            });


            _context = context;
        }
        public async Task<IUploadProgress> UploadToSurveyFolder(string fileUrl, string mediaContentType, string phoneNumber)
        {
            //James' nav folder
            // var bastaSurveyFolderId = "18Gb5xZrQvHrMnMAg7xQFbabzfnIm9hSM";
            var bastaSurveyFolderId = "1mAopWyRImJZThMO75_DP93SqI5wyxwjF";
            var imageFiles = await GetImageFilesInFolder(bastaSurveyFolderId);

            var phonePicIndex = 0;
            if (imageFiles.Files.Count > 0)
            {

                var imageNames = imageFiles.Files.Where(f => f.Name.StartsWith(phoneNumber + "-")).Select(n => int.Parse(n.Name.Split("-").Last()));

                if (imageNames.Any())
                {
                    phonePicIndex = imageNames.Max() + 1;
                }
            };

            var file = new File()
            {
                Name = $"{phoneNumber}-{phonePicIndex}",
                MimeType = mediaContentType,
                Parents = new List<string>() { bastaSurveyFolderId },

            };

            await using var stream = _webClient.OpenRead(fileUrl);
            var request = _driveService.Files.Create(file, stream, mediaContentType);
            return await request.UploadAsync();
        }

        public async Task UploadMedia(string fileUrl, string mediaContentType, string phoneNumber)
        {
            // Create a folder for every property
            // propertyabbreviation_unit#_last 4 digits of phone_timestamp.jpg
            //
            // Folders:
            // Property Name/Unit Number/phone number/imagename.jpg
            // Property Name/All Images/imagename.jpg

            /* TODO: This should be broken up into 2 stages.
             * 1. Find the folder structure name. This could be an array of paths like
             *   ["Property Name", "Unit 302", "512-555-5555"]
             * 2. Create a function that accepts the folder structure and then builds/finds
             *    the the folders, returning the final folder as the parent of the file
            */
            var parentFolder = await GetParentFolder(phoneNumber);


            // TODO: Give this real attributes
            var file = new File
            {
                Name = "FILE NAME",
                Description = "A GOOD FILE DESCRIPTION",
                Parents = new List<string> { parentFolder.Id }
            };
            Console.WriteLine(fileUrl);

            // TODO: Handle Multiple parent folder situations
            await using var stream = _webClient.OpenRead(fileUrl);
            var request = _driveService.Files.Create(file, stream, mediaContentType);
            var progress = await request.UploadAsync();
            Console.WriteLine(progress.Status);
        }

        private async Task<File> GetParentFolder(string phoneNumber)
        {
            // Find the matching phone number and include all the references we need
            // all the way up to the property
            var phone = await _context.Phones
                .Include(x => x.Tenants)
                .ThenInclude(x => x.CurrentResidence)
                .ThenInclude(x => x!.Property)
                .ThenInclude(x => x!.Address)
                .Include(x => x.Tenants)
                .ThenInclude(x => x.CurrentResidence)
                .ThenInclude(x => x!.Address)
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            // var residences = phone.Tenants
            //     .Select(x => x.CurrentResidence)
            //     .Where(x => x != null)
            //     .Select(e => e!);
            //
            // var properties = residences.Select(x => x.Property);

            var parentFolderId = "1zLh0QdIdHHDIMWS-U0SefDh0ZZhQAdA9";

            var folders = await GetFolders();
            Console.WriteLine(folders.Files.Select(x => $"{x.Name}: {x.Id}: {x.Owners}").Aggregate((f, s) => $"{f}, {s}"));

            // If there are no tenants attached to this phone number, create just a
            // folder for the phone number at the top level
            if (!phone.Tenants.Any())
            {
                return await GetOrCreateFolder(folders, phoneNumber, parentFolderId);
            }

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(phone.Tenants, options: new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));

            // Ok, now we can guarantee that we have at least 1 tenant attached to this number
            var tenants = phone.Tenants.Select(x => new
            {
                tenantName = x.Name,
                residenceAddress = x.CurrentResidence?.Address.Line2 ?? x.CurrentResidence?.Address.Line1,
                property = x.CurrentResidence?.Property?.Name
            });

            foreach (var t in tenants)
            {
                // There is no residence
                if (t.residenceAddress == null)
                {
                    return await GetOrCreateFolder(folders, t.tenantName, parentFolderId);
                }

                if (t.property == null)
                {
                    return await GetOrCreateFolder(folders, t.residenceAddress, parentFolderId);
                }

                return await GetOrCreateFolder(folders, t.property, parentFolderId);
            }

            return await GetOrCreateFolder(folders, "TEST FOLDER", parentFolderId);

            // What are the rules? 
            // 1. If there is no known tenant, create a folder for the phone number at the top level and stick it in there
            // 2. If we have a tenant, but no residence, create a folder (if not found elsewhere) for the tenant,
            //    and stick it at the top level
            // 3. If we have a residence, but no attached property, create the residence folder and stick at the top level
            // 4. If we have a property that has not already been created, create it, then stick image in as follows:
            //    Property Name
            //    - Residence Address Line
            //      - Tenant Name
            //        - Picture

        }

        /**
         * Given a list of folders and the name of a folder, attempt to find the folder. If it exists,
         * it will return it. If it does not exist, it will create a new folder and set its parent to be
         * the passed in parentId
         */
        private async Task<File> GetOrCreateFolder(FileList folders, string name, string parentId)
        {
            var folder = folders.Files.FirstOrDefault(x => x.Name == name);

            if (folder != null) return folder;

            folder = new File();
            folder.Name = name;
            folder.MimeType = "application/vnd.google-apps.folder";
            folder.Parents = new List<string> { parentId };

            return await _driveService.Files.Create(folder).ExecuteAsync();

        }
        /// <summary>
        /// Returns all folders the application credential has access to, regardless of which drive folders are on 
        /// </summary>
        /// <returns></returns>
        private async Task<FileList> GetFolders()
        {
            var listQuery = _driveService.Files.List();
            listQuery.Q = "mimeType = 'application/vnd.google-apps.folder'";

            // TODO: Might need to do some pagination here

            return await listQuery.ExecuteAsync();
        }
        private Task<FileList> GetImageFilesInFolder(string folderId)
        {
            var listQuery = _driveService.Files.List();
            listQuery.Q = $"'{folderId}' in parents and (mimeType = 'image/jpeg' or mimeType = 'image/png')";

            return listQuery.ExecuteAsync();
        }
    }
}