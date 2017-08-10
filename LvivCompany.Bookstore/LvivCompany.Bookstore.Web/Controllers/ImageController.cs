using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.Extensions.Configuration;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class ImageController: Controller
    {
        private readonly IConfiguration configuration;

        public ImageController(IConfiguration configuration )
        {
            this.configuration = configuration;
        }

        public async Task<string> UploadFileToBlob(IFormFile file, string fileName)
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


        public IActionResult Index()
        {
            ImageViewModel filesURL = new ImageViewModel();
            return View(filesURL);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ICollection<IFormFile> files)
        {
            ImageViewModel filesURL = new ImageViewModel();
            var file = files.FirstOrDefault();
            string Extension = Path.GetExtension(file.FileName);
            var filesUrl = await UploadFileToBlob(file, RandomString(10)+ Extension);
            filesURL.ImageFile = filesUrl;
               
            return View(filesURL);
        }

        public static string RandomString(int length)
        { 
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
