using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film;

[UsedImplicitly]
public class FilmUpdateCommandValidator : AbstractValidator<FilmUpdateCommand>
{
    public FilmUpdateCommandValidator(
        FilmExistsValidator filmExistsValidator, 
        FilmManagementValidator filmManagementValidator)
    {
        RuleFor(cmd => cmd.Id)
            .SetValidator(filmExistsValidator)
            .DependentRules(() =>
            {
                RuleFor(cmd => new FilmManagementValidatorModel(
                        cmd.Model.Year, 
                        cmd.Model.Budget, 
                        cmd.Model.DurationInMinutes,
                        cmd.Model.Photo!,
                        cmd.Model.GenreId, 
                        cmd.Model.DirectorId,
                        cmd.Model.ActorIds))
                    .SetValidator(filmManagementValidator);
            });
    }
}