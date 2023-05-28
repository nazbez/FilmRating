namespace FilmRating.Infrastructure.AzureStorage;

public interface IAzureStorageService
{
    Task<BlobResponse> Upload(string blobFileName, IFormFile file);
    Task<Blob?> Download(string blobFileName);
    Task<BlobResponse> Delete(string blobFileName);
}