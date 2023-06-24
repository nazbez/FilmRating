using FilmRating.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.Results;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Artist;

[UsedImplicitly]
public class ArtistDeleteCommandValidator : AbstractValidator<ArtistDeleteCommand>
{
    private readonly ArtistExistsValidator artistExistsValidator;
    private readonly IRepository<ArtistEntity, Guid> artistRepository;
    
    public ArtistDeleteCommandValidator(
        ArtistExistsValidator artistExistsValidator,
        IRepository<ArtistEntity, Guid> artistRepository)
    {
        this.artistExistsValidator = artistExistsValidator;
        this.artistRepository = artistRepository;
    }
    
    public override ValidationResult Validate(ValidationContext<ArtistDeleteCommand> context)
    {
        var artist = artistRepository.Find(
                new ArtistGetByIdSpecification(context.InstanceToValidate.Id, withDirectorFilms: true, withActorFilms: true))
            .First();

        RuleFor(cmd => cmd.Id)
            .SetValidator(artistExistsValidator)
            .DependentRules(() =>
            {
                RuleFor(a => a)
                    .Must(_ => !artist.DirectorFilms.Any() && !artist.ActorFilms.Any())
                    .WithMessage(cmd => ErrorMessage(artist.ActorFilms, artist.DirectorFilms));
            });
        
        return base.Validate(context);
    }

    private static string ErrorMessage(
        IEnumerable<FilmEntity> artistActorFilms,
        IEnumerable<FilmEntity> artistDirectorFilms)
    {
        var films = artistActorFilms.Select(x => x.Title)
            .Union(artistDirectorFilms.Select(x => x.Title));

        return $"Cannot delete this artist. Artist has related films: {string.Join(", ", films)}";
    }
}