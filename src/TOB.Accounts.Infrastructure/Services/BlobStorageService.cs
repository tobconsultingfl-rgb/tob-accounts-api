using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using TOB.Accounts.Domain.AppSettings;

namespace TOB.Accounts.Infrastructure.Services;

/// <summary>
/// Implementation of blob storage service using Azure Blob Storage
/// </summary>
public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly AzureStorageOptions _storageOptions;

    public BlobStorageService(IOptions<AzureStorageOptions> storageOptions)
    {
        _storageOptions = storageOptions.Value;
        _blobServiceClient = new BlobServiceClient(_storageOptions.ConnectionString);
    }

    public async Task<string> UploadFileAsync(string containerName, string blobName, Stream fileStream, string contentType, CancellationToken cancellationToken = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        // Create container if it doesn't exist
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.None, cancellationToken: cancellationToken);

        var blobClient = containerClient.GetBlobClient(blobName);

        // Set content type
        var blobHttpHeaders = new BlobHttpHeaders
        {
            ContentType = contentType
        };

        // Upload the file
        await blobClient.UploadAsync(fileStream, new BlobUploadOptions
        {
            HttpHeaders = blobHttpHeaders
        }, cancellationToken);

        return blobClient.Uri.ToString();
    }

    public async Task<Stream> DownloadFileAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        var response = await blobClient.DownloadStreamingAsync(cancellationToken: cancellationToken);

        var memoryStream = new MemoryStream();
        await response.Value.Content.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;

        return memoryStream;
    }

    public async Task<bool> DeleteFileAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        var response = await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        return response.Value;
    }

    public string GetFileUrl(string containerName, string blobName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);
        return blobClient.Uri.ToString();
    }
}
