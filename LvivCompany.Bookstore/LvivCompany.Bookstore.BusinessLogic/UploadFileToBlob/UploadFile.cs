using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LvivCompany.Bookstore.BusinessLogic
{
    public class UploadFile
    {
        
        private static async Task<string> UploadFileToBlob(IFormFile file, string fileName, IConfiguration configuration)
        {
            var test = configuration["ContainerCS"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(test);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            using (var fileStream = file.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return blockBlob.Uri.AbsoluteUri;
        }

        private static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<string> RetrieveFilePath(IFormFile file, IConfiguration configuration)
        {
            string Extension = Path.GetExtension(file.FileName);
            var filesUrl = await UploadFileToBlob(file, RandomString(10) + Extension, configuration);
            return filesUrl;
        }
    }
}