using FilmRating.Features.Film.Artist;
using FilmRating.Features.Film.Genre;
using FilmRating.Infrastructure.Extensions;
using FilmRating.Infrastructure.Repository;
using FluentValidation;

namespace FilmRating.Features.Film;

public record FilmManagementValidatorModel(
    int Year,
    decimal Budget,
    int DurationInMinutes,
    IFormFile? Photo,
    int GenreId,
    Guid DirectorId,
    IEnumerable<Guid> ActorIds);

public class FilmManagementValidator : AbstractValidator<FilmManagementValidatorModel>
{
    public FilmManagementValidator(
        IRepository<GenreEntity, int> genreRepository,
        IRepository<ArtistEntity, Guid> artistRepository,
        FilmConfiguration filmConfiguration)
    {
        RuleFor(m => m.Year)
            .GreaterThanOrEqualTo(filmConfiguration.MinimumYear)
            .LessThanOrEqualTo(DateTimeOffset.UtcNow.Year);

        RuleFor(m => m.Budget)
            .GreaterThanOrEqualTo(filmConfiguration.MinimumBudget);

        RuleFor(m => m.DurationInMinutes)
            .GreaterThan(filmConfiguration.MinimumDurationInMinutes);

        When(m => m.Photo is not null, () =>
        {
            RuleFor(m => m.Photo!.FileName)
                .Must(f => filmConfiguration.AllowedPhotoExtensions.Any(x => x == f.GetExtension()))
                .WithMessage(PhotoErrorMessage(filmConfiguration.AllowedPhotoExtensions));
        });

        RuleFor(m => m.GenreId)
            .Must(id =>
            {
                var genre = genreRepository.FindById(id);

                return genre is not null;
            })
            .WithMessage(m => GenreErrorMessage(m.GenreId));

        RuleFor(m => m.DirectorId)
            .Must(id =>
            {
                var artist = artistRepository.Find(
                        new ArtistGetByIdSpecification(id, withRoles: true))
                    .FirstOrDefault();

                return artist is not null && artist.Roles.Any(x => x.Id == filmConfiguration.DirectorRoleId);
            })
            .WithMessage(m => DirectorErrorMessage(m.DirectorId));

        RuleFor(m => m.ActorIds)
            .Must(ids =>
            {
                var idsList = ids.ToList();
                
                var artists = artistRepository.Find(
                        new ArtistGetByIdsSpecification(idsList, withRoles: true))
                    .ToList();

                return artists.All(x => x.Roles.Any(r => r.Id == filmConfiguration.ActorRoleId))
                       && artists.Count == idsList.Count;
            })
            .WithMessage(_ => ActorsErrorMessage());
    }

    private static string PhotoErrorMessage(string[] extensions) =>
        $"Uploaded photo must have these extensions: {string.Join(';', extensions)}";

    private static string GenreErrorMessage(int id) =>
        $"There is no genre with id = {id}";

    private static string DirectorErrorMessage(Guid id) =>
        $"Artist with id = {id} does not exist or not have required role to be director";

    private static string ActorsErrorMessage() =>
        "All actors must exist and have required role";
}