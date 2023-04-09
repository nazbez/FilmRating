namespace FilmRating.Infrastructure.Extensions;

public static class StringExtensions
{
    public static string GetFileName(this string path) => 
        Path.GetFileName(path);

    public static string GetExtension(this string fileName) =>
        Path.GetExtension(fileName);
}