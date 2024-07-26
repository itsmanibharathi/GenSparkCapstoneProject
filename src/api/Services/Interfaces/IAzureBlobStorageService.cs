namespace api.Services.Interfaces
{
    public interface IAzureBlobStorageService
    {
        public Task<string> UploadImageAsync(string containerName, string blobName, IFormFile file);
        public Task<string> UpdateImageAsync(string containerName, string blobName, IFormFile file);
    }
}