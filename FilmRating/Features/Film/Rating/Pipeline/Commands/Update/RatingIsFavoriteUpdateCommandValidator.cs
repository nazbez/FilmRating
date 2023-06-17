using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Rating;

[UsedImplicitly]
public class RatingIsFavoriteUpdateCommandValidator : AbstractValidator<RatingIsFavoriteUpdateCommand>
{
    public RatingIsFavoriteUpdateCommandValidator(RatingIsFavoriteValidator ratingValidator)
    {
        RuleFor(cmd => new RatingIsFavoriteValidatorModel(
                cmd.FilmId,
                cmd.IsFavorite))
            .SetValidator(ratingValidator);
    }
}