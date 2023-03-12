using FilmRating.Features.Film.Artist;
using FilmRating.Features.Film.Genre;
using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film;

public class FilmEntity : IEntity<int>
{
    public int Id { get; private set; }
    public string Title { get; private set; } = null!;
    public int Year { get; private set; }
    public string ShortDescription { get; private set; } = null!;
    public decimal Budget { get; private set; }
    public TimeSpan Duration { get; private set; }
    public double Rating { get; private set; }
    public int GenreId { get; private set; }
    public Guid DirectorId { get; private set; }

    public GenreEntity? Genre { get; private set; } = null!;
    public ArtistEntity? Director { get; private set; } = null!;

    public ICollection<ArtistEntity> Actors { get; private set; } = new HashSet<ArtistEntity>();

    public void UpdateTitle(string title) =>
        Title = title;

    public void UpdateYear(int year) =>
        Year = year;

    public void UpdateShortDescription(string shortDescription) =>
        ShortDescription = shortDescription;

    public void UpdateBudget(decimal budget) =>
        Budget = budget;

    public void UpdateDuration(TimeSpan duration) =>
        Duration = duration;

    public void UpdateGenreId(int genreId) =>
        GenreId = genreId;

    public void UpdateDirectorId(Guid directorId) =>
        DirectorId = directorId;

    public void UpdateActors(ICollection<ArtistEntity> actors) =>
        Actors = actors;

    public static FilmEntity Create(string title,
        int year,
        string shortDescription,
        decimal budget,
        TimeSpan duration,
        int genreId,
        Guid directorId,
        ICollection<ArtistEntity> actors) =>
        new()
        {
            Title = title,
            Year = year,
            ShortDescription = shortDescription,
            Budget = budget,
            Duration = duration,
            GenreId = genreId,
            DirectorId = directorId,
            Actors = actors
        };
}