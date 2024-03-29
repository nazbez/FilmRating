﻿using FilmRating.Features.Film.Artist;
using FilmRating.Features.Film.Genre;
using FilmRating.Features.Film.Rating;
using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film;

public class FilmEntity : Entity<int>
{
    public string Title { get; private set; } = null!;
    public int Year { get; private set; }
    public string ShortDescription { get; private set; } = null!;
    public decimal Budget { get; private set; }
    public TimeSpan Duration { get; private set; }
    public double Rating { get; private set; }
    public string PhotoPath { get; private set; } = null!;
    public int GenreId { get; private set; }
    public Guid DirectorId { get; private set; }

    public GenreEntity? Genre { get; private set; } = null!;
    public ArtistEntity? Director { get; private set; } = null!;

    public ICollection<ArtistEntity> Actors { get; private set; } = new HashSet<ArtistEntity>();
    public ICollection<RatingEntity> Ratings { get; private set; } = new HashSet<RatingEntity>();

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

    public void UpdatePhotoPath(string photoPath) =>
        PhotoPath = photoPath;

    public void UpdateGenreId(int genreId) =>
        GenreId = genreId;

    public void UpdateDirectorId(Guid directorId) =>
        DirectorId = directorId;

    public void UpdateActors(ICollection<ArtistEntity> actors) =>
        Actors = actors;

    public void UpdateRating(IEnumerable<int> rates)
    {
        Rating = rates.Average();
    }

    public static string GetBlobName(string title, int year) =>
        $"{title}_{year}";

    public static FilmEntity Create(
        string title,
        int year,
        string shortDescription,
        decimal budget,
        TimeSpan duration,
        string photoPath,
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
            PhotoPath = photoPath,
            GenreId = genreId,
            DirectorId = directorId,
            Actors = actors
        };
}