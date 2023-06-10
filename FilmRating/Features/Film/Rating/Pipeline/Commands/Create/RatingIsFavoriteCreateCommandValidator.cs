using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using FluentValidation;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Rating;

[UsedImplicitly]
public class RatingIsFavoriteCreateCommandValidator : AbstractValidator<RatingIsFavoriteCreateCommand>
{
    public RatingIsFavoriteCreateCommandValidator(
        RatingIsFavoriteValidator ratingValidator, 
        IRepository<RatingEntity, int> ratingRepository, 
        IUserProvider userProvider)
    {
        RuleFor(cmd => new RatingIsFavoriteValidatorModel(cmd.FilmId, cmd.IsFavorite))
            .SetValidator(ratingValidator)
            .DependentRules(() =>
            {
                RuleFor(cmd => cmd.FilmId)
                    .Must(filmId =>
                    {
                        var userId = userProvider.GetUserId()!;
                        return !ratingRepository.Contains(new RatingGetByUserIdAndFilmId(filmId, userId));
                    })
                    .WithMessage(cmd => DuplicateRateErrorMessage(cmd.FilmId));
            });
    }
    
    private static string DuplicateRateErrorMessage(int filmId) =>
        $"User can rate film only once, film id = {filmId}";
}