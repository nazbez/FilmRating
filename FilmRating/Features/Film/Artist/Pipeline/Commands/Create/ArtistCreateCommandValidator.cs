using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Artist;

[UsedImplicitly]
public class ArtistCreateCommandValidator : AbstractValidator<ArtistCreateCommand>
{
    public ArtistCreateCommandValidator(ArtistRoleIdsValidator artistRoleIdsValidator)
    {
        RuleFor(cmd => cmd.RoleIds)
            .SetValidator(artistRoleIdsValidator);
    } 
}