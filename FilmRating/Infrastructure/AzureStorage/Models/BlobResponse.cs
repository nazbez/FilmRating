namespace FilmRating.Infrastructure.AzureStorage;

public class BlobResponse
{
    public string? Status { get; set; }
    public bool Error { get; set; }
    public Blob Blob { get; set; }

    public BlobResponse()
    {
        Blob = new Blob();
    }
}