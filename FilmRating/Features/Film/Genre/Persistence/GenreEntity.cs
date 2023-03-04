using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Genre;

public class GenreEntity : IEntity<int>
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;

    public static GenreEntity Create(int id, string name) =>
        new()
        {
            Id = id,
            Name = name
        };
}