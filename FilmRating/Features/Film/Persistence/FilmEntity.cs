using FilmRating.Features.Film.Artist;
using FilmRating.Features.Film.Genre;
using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film;

public class FilmEntity : IEntity<int>
{
    public int Id { get; private set; }
    public string Title { get; private set; } = null!;
    public DateTimeOffset Year { get; private set; }
    public string ShortDescription { get; private set; } = null!;
    public decimal Budget { get; private set; }
    public TimeSpan Duration { get; private set; }
    public double Rating { get; private set; }
    public int GenreId { get; private set; }
    public Guid DirectorId { get; private set; }

    public GenreEntity? Genre { get; private set; } = null!;
    public ArtistEntity? Director { get; private set; } = null!;

    public ICollection<ArtistEntity> Actors { get; private set; } = new HashSet<ArtistEntity>();
}