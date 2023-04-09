namespace FilmRating.Infrastructure.AzureStorage;

public interface IAzureStorageService
{
    Task<BlobResponse> Upload(IFormFile file);
    Task<Blob?> Download(string blobFileName);
    Task<BlobResponse> Delete(string blobFileName);
}