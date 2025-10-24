namespace TOB.Accounts.Infrastructure.Services;

/// <summary>
/// Service for managing blob storage operations
/// </summary>
public interface IBlobStorageService
{
    /// <summary>
    /// Upload a file to blob storage
    /// </summary>
    /// <param name="containerName">Container name</param>
    /// <param name="blobName">Blob name (unique identifier)</param>
    /// <param name="fileStream">File stream</param>
    /// <param name="contentType">Content type</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Blob URL</returns>
    Task<string> UploadFileAsync(string containerName, string blobName, Stream fileStream, string contentType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Download a file from blob storage
    /// </summary>
    /// <param name="containerName">Container name</param>
    /// <param name="blobName">Blob name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>File stream</returns>
    Task<Stream> DownloadFileAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a file from blob storage
    /// </summary>
    /// <param name="containerName">Container name</param>
    /// <param name="blobName">Blob name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted, false if not found</returns>
    Task<bool> DeleteFileAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the URL of a blob
    /// </summary>
    /// <param name="containerName">Container name</param>
    /// <param name="blobName">Blob name</param>
    /// <returns>Blob URL</returns>
    string GetFileUrl(string containerName, string blobName);
}
