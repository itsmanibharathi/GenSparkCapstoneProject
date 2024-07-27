using api.Exceptions;
using api.Services.Interfaces;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace API.Services
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        
        public AzureBlobStorageService()
        {
        }

        public async Task<string> UploadFileAsync(string containerName, string blobName, IFormFile file)
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING") ?? throw new EnvironmentVariableUndefinedException("AZURE_STORAGE_CONNECTION_STRING");
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await blobClient.UploadAsync(memoryStream, true);
                }

                return blobClient.Uri.ToString();
            }
            catch (EnvironmentVariableUndefinedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to upload image", ex);
            }
        }

        public async Task<string> UpdateFileAsync(string containerName, string blobName, IFormFile file)
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING") ?? throw new EnvironmentVariableUndefinedException("AZURE_STORAGE_CONNECTION_STRING");
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await blobClient.UploadAsync(memoryStream, true);
                }

                return blobClient.Uri.ToString();
            }
            catch (EnvironmentVariableUndefinedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to upload image", ex);
            }
        }

        public async Task<bool> DeleteFileAsync(string containerName, string blobName)
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING") ?? throw new EnvironmentVariableUndefinedException("AZURE_STORAGE_CONNECTION_STRING");
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);
                return await blobClient.DeleteIfExistsAsync();
            }
            catch (EnvironmentVariableUndefinedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to delete image", ex);
            }
        }
    }
}
