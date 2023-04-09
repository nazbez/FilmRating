namespace FilmRating.Infrastructure.AzureStorage;

public class AzureStorageConfiguration
{
    public string ConnectionString { get; set; } = null!;
    public string BlobContainerName { get; set; } = null!; 
}