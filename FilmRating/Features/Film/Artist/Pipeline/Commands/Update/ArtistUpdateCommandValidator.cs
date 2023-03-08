using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Artist;

[UsedImplicitly]
public class ArtistUpdateCommandValidator : AbstractValidator<ArtistUpdateCommand>
{
    public ArtistUpdateCommandValidator(
        ArtistExistsValidator artistExistsValidator,
        ArtistRoleIdsValidator artistRoleIdsValidator)
    {
        RuleFor(cmd => cmd.Id)
            .SetValidator(artistExistsValidator);
        
        RuleFor(cmd => cmd.Model.RoleIds)
            .SetValidator(artistRoleIdsValidator);
    } 
}