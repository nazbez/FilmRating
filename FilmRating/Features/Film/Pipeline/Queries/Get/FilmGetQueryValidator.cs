using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film;

[UsedImplicitly]
public class FilmGetQueryValidator : AbstractValidator<FilmGetQuery>
{
    public FilmGetQueryValidator(FilmExistsValidator filmExistsValidator)
    {
        RuleFor(cmd => cmd.Id)
            .SetValidator(filmExistsValidator);
    }
}