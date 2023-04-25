using FilmRating.Infrastructure.Repository;
using FluentValidation;

namespace FilmRating.Features.Film;

public class FilmGetQueryValidator : AbstractValidator<FilmGetQuery>
{
    public FilmGetQueryValidator(FilmExistsValidator filmExistsValidator)
    {
        RuleFor(cmd => cmd.Id)
            .SetValidator(filmExistsValidator);
    }
}