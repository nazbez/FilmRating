using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film;

[UsedImplicitly]
public class FilmGetRatingQueryValidator : AbstractValidator<FilmGetRatingQuery>
{
    public FilmGetRatingQueryValidator(FilmExistsValidator filmExistsValidator)
    {
        RuleFor(cmd => cmd.Id)
            .SetValidator(filmExistsValidator);
    }
}