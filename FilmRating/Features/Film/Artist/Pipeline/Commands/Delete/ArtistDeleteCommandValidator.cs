using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Artist;

[UsedImplicitly]
public class ArtistDeleteCommandValidator : AbstractValidator<ArtistDeleteCommand>
{
    public ArtistDeleteCommandValidator(ArtistExistsValidator artistExistsValidator)
    {
        RuleFor(cmd => cmd.Id)
            .SetValidator(artistExistsValidator);
    }
}