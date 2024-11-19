using Azure.Storage.Blobs;
using SecretSanta_Core.BusinessLogic;
using System.Drawing;
using Microsoft.Extensions.Options;
using SecretSanta_Core.Models;

namespace SecretSanta_Core.Services
{
    public class StorageService
    {

        private readonly AppSettings _appSettings;

        public StorageService(IOptions<AppSettings> appSettings)
        {
           _appSettings = appSettings.Value;
        }

        public void AddToAzureStorage(IFormFile file, string fileName, string containerName)
        {
            var _task = Task.Run(() => this.UploadFileToBlobAsync(file, fileName, containerName));
            _task.Wait();
        }

        public string AddImageToAzureStorage(IFormFile file, string containerName, int width, int height)
        {
            var _task = Task.Run(() => this.UploadImageToBlobAsync(file, containerName, width, height));
            _task.Wait();
            return _task.Result;
        }

        private async Task UploadFileToBlobAsync(IFormFile file, string strFileName, string containerName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_appSettings.StorageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(strFileName);
            using MemoryStream memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream, true);

        }

        public List<BlobImageModel> GetBlobs(string containerName)
        {

            var list = new List<BlobImageModel>();
            BlobServiceClient blobServiceClient = new BlobServiceClient(_appSettings.StorageConnectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                var blobs = containerClient.GetBlobs();
                foreach (var blob in blobs)
                {
                    var blobImage = new BlobImageModel();
                    BlobClient blobClient = containerClient.GetBlobClient(blob.Name);
                    blobImage.BlobImageUri = blobClient.Uri.AbsoluteUri;
                    blobImage.BlobImageName = blob.Name;
                    blobImage.LastModifiedDate = BusinessMethods.GetLocalDateTime(blob.Properties.LastModified.Value.UtcDateTime);
                    list.Add(blobImage);
            }

            return list;
        }

        private async Task<string> UploadImageToBlobAsync(IFormFile file, string containerName, int width, int height)
        {
            var name = Guid.NewGuid().ToString() + BusinessLogic.BusinessMethods.GetFileExtension(file.FileName);

            BlobServiceClient blobServiceClient = new BlobServiceClient(_appSettings.StorageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(name);

            var bm = Image.FromStream(file.OpenReadStream());
            bm = BusinessMethods.ReorientBitmap(bm);
            bm = BusinessMethods.ResizeBitmap((Bitmap)bm, new Size { Width = width, Height = height }, bm.Size);

            using MemoryStream memoryStream = new MemoryStream();
            bm.Save(memoryStream,BusinessMethods.GetMimeType(name));
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream, true);
            return name;
        }
    }
}
