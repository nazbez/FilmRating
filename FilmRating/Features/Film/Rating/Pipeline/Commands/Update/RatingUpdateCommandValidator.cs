using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Rating;

[UsedImplicitly]
public class RatingUpdateCommandValidator : AbstractValidator<RatingUpdateCommand>
{
    public RatingUpdateCommandValidator(RatingValidator ratingValidator)
    {
        RuleFor(cmd => new RatingValidatorModel(
                cmd.FilmId,
                cmd.Rate))
            .SetValidator(ratingValidator);
    }
}