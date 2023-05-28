using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace FilmRating.Infrastructure.AzureStorage;

public class AzureStorageService : IAzureStorageService
{
    private readonly AzureStorageConfiguration azureStorageConfiguration;
    private readonly ILogger<AzureStorageService> logger;

    public AzureStorageService(
        AzureStorageConfiguration azureStorageConfiguration,
        ILogger<AzureStorageService> logger)
    {
        this.azureStorageConfiguration = azureStorageConfiguration;
        this.logger = logger;
    }

    public async Task<BlobResponse> Upload(string blobFileName, IFormFile file)
    {
        BlobResponse response = new();
        
        var container = new BlobContainerClient(
            azureStorageConfiguration.ConnectionString,
            azureStorageConfiguration.BlobContainerName);
        
        try 
        {
            var client = container.GetBlobClient(blobFileName);
            
            await using (var data = file.OpenReadStream())
            {
                await client.UploadAsync(data);
            }
            
            response.Status = $"File {blobFileName} Uploaded Successfully";
            response.Error = false;
            response.Blob.Uri = client.Uri.AbsoluteUri;
            response.Blob.Name = client.Name;

        }
        catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
        {
            logger.LogError(ex, "File with name {FileName} already exists in container. Set another name to store the file in the container: '{ContainerName}.'", blobFileName, azureStorageConfiguration.BlobContainerName);
            response.Status = $"File with name {blobFileName} already exists. Please use another name to store your file.";
            response.Error = true;
            return response;
        } 
        catch (RequestFailedException ex)
        {
            logger.LogError(ex, "Unhandled Exception. ID: {StackTrace} - Message: {Message}", ex.StackTrace, ex.Message);
            response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
            response.Error = true;
            return response;
        }

        return response;
    }

    public async Task<Blob?> Download(string blobFileName)
    {
        var container = new BlobContainerClient(
            azureStorageConfiguration.ConnectionString,
            azureStorageConfiguration.BlobContainerName);

        try
        {
            var file = container.GetBlobClient(blobFileName);

            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();

                var content = await file.DownloadContentAsync();

                var contentType = content.Value.Details.ContentType;

                return new Blob { Content = data, Name = blobFileName, ContentType = contentType };
            }
        }
        catch (RequestFailedException ex) when(ex.ErrorCode == BlobErrorCode.BlobNotFound)
        {
            logger.LogError(ex, "File {Filename} was not found.", blobFileName);
        }

        return null;
    }

    public async Task<BlobResponse> Delete(string blobFileName)
    {
        var container = new BlobContainerClient(
            azureStorageConfiguration.ConnectionString,
            azureStorageConfiguration.BlobContainerName);
        
        var file = container.GetBlobClient(blobFileName);

        try
        {
            await file.DeleteAsync();
        }
        catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
        {
            logger.LogError(ex, "File {Filename} was not found.", blobFileName);
            return new BlobResponse { Error = true, Status = $"File with name {blobFileName} not found." };
        }

        return new BlobResponse { Error = false, Status = $"File: {blobFileName} has been successfully deleted." };
    }
}