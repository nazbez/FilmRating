using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film;

[UsedImplicitly]
public class FilmCreateCommandValidator : AbstractValidator<FilmCreateCommand>
{
    public FilmCreateCommandValidator(FilmManagementValidator filmManagementValidator)
    {
        RuleFor(cmd => new FilmManagementValidatorModel(
                cmd.Year, 
                cmd.Budget, 
                cmd.DurationInMinutes,
                cmd.GenreId, 
                cmd.DirectorId,
                cmd.ActorIds))
            .SetValidator(filmManagementValidator);
    }
}