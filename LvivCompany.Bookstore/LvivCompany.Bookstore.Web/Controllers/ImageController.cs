using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Web;
using System.Net.Http.Headers;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class ImageController: Controller
    {


        public async Task<string> UploadFileToBlob(IFormFile file, string fileName)
        {
            try
            {

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=lv251bookstore;AccountKey=QVHI9huflou7/ERBeqx/3ZX6lllGMT8Tb/CpteX/BfcbjBTfOjBTaBf11Au9t0cPfseaaExHa2pxQGeMONzVBw==;EndpointSuffix=core.windows.net");

                // Create a blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Get a reference to a container 
                CloudBlobContainer container = blobClient.GetContainerReference("images");
                //https://lv251bookstore.blob.core.windows.net/
                // If container doesn’t exist, create it.
                await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);

                // Get a reference to a blob 
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                // Create or overwrite the blob with the contents of a local file
                using (var fileStream = file.OpenReadStream()) // file.OpenReadStream())
                {
                    await blockBlob.UploadFromStreamAsync(fileStream);
                }

                return blockBlob.Uri.AbsoluteUri;
            }
            catch (Exception x)
            {
                return string.Empty;
            }
        }

        public async Task<IActionResult> Index()
        {
            ImagesModel filesURL = new ImagesModel();
            return View(filesURL);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ICollection<IFormFile> files)
        {
            ImagesModel filesURL = new ImagesModel();
            // var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            var uploads = Path.Combine("E:/", "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileUrl =await UploadFileToBlob(file, RandomString(10)+".jpg");
                    if(!string.IsNullOrEmpty(fileUrl))
                    filesURL.FilesCollection.Add(fileUrl);
                }
            }
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

    public class ImagesModel
    {
        public List<string> FilesCollection { get; set; }
        public ImagesModel()
        {
            FilesCollection = new List<string>();
        }
    }
}
