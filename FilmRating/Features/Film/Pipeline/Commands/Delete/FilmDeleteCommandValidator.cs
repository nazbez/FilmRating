using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film;

[UsedImplicitly]
public class FilmDeleteCommandValidator : AbstractValidator<FilmDeleteCommand>
{
    public FilmDeleteCommandValidator(FilmExistsValidator filmExistsValidator)
    {
        RuleFor(cmd => cmd.Id)
            .SetValidator(filmExistsValidator);
    }
}