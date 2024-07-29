namespace api.Services.Interfaces
{
    public interface IAzureBlobStorageService
    {
        public Task<string> UploadFileAsync(string containerName, IFormFile file);
        public Task<string> UpdateFileAsync(string containerName, string blobName, IFormFile file);
        public Task<bool> DeleteFileAsync(string containerName, string blobName);
    }
}