// TODO: Check async problems
/*using FilmRating.Features.Film.Rating.Pipeline.Validators;
using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Rating.Pipeline.Commands;

[UsedImplicitly]
public class RatingCreateCommandValidator : AbstractValidator<RatingCreateCommand>
{
    public RatingCreateCommandValidator(RatingValidator ratingValidator)
    {
        RuleFor(cmd => new RatingValidatorModel(
                cmd.FilmId,
                cmd.UserId,
                cmd.Rate))
            .SetValidator(ratingValidator);
    }
}*/