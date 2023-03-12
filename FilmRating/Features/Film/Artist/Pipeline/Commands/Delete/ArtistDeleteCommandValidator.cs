using FilmRating.Infrastructure.Repository;
using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Artist;

[UsedImplicitly]
public class ArtistDeleteCommandValidator : AbstractValidator<ArtistDeleteCommand>
{
    public ArtistDeleteCommandValidator(
        ArtistExistsValidator artistExistsValidator,
        IRepository<ArtistEntity, Guid> artistRepository)
    {
        RuleFor(cmd => cmd.Id)
            .SetValidator(artistExistsValidator)
            .DependentRules(() =>
            {
                RuleFor(cmd => cmd.Id)
                    .Must(id =>
                    {
                        var artist = artistRepository.Find(
                                new ArtistGetByIdSpecification(id, withDirectorFilms: true, withActorFilms: true))
                            .First();

                        return !artist.DirectorFilms.Any() || artist.ActorFilms.Any();
                    })
                    .WithMessage(cmd => ErrorMessage(cmd.Id));
            });
    }
    
    private static string ErrorMessage(Guid id) => $"Cannot delete artist with {id}. Artist has related films";
}