using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Rating;

[UsedImplicitly]
public class RatingIsFavouriteUpdateCommandValidator : AbstractValidator<RatingIsFavouriteUpdateCommand>
{
    public RatingIsFavouriteUpdateCommandValidator(RatingIsFavoriteValidator ratingValidator)
    {
        RuleFor(cmd => new RatingIsFavoriteValidatorModel(cmd.FilmId))
            .SetValidator(ratingValidator);
    }
}