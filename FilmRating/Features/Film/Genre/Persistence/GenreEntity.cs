using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Genre;

public class GenreEntity : Entity<int>
{
    public string Name { get; private set; } = null!;

    public ICollection<FilmEntity> Films { get; private set; } = new HashSet<FilmEntity>();

    public static GenreEntity Create(int id, string name) =>
        new()
        {
            Id = id,
            Name = name
        };
}