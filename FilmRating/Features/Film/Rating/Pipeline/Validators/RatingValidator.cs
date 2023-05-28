using FilmRating.Infrastructure.Repository;
using FluentValidation;

namespace FilmRating.Features.Film.Rating;

public record RatingValidatorModel(int FilmId, int Rate);

public class RatingValidator : AbstractValidator<RatingValidatorModel>
{
    public RatingValidator(
        IRepository<FilmEntity, int> filmRepository,
        RatingConfiguration ratingConfiguration)
    {
        RuleFor(r => r.FilmId)
            .Must(id => filmRepository.Contains(x => x.Id == id))
            .WithMessage(r => FilmErrorMessage(r.FilmId))
            .DependentRules(() =>
            {
                RuleFor(r => r.Rate)
                    .GreaterThanOrEqualTo(ratingConfiguration.MinRate)
                    .LessThanOrEqualTo(ratingConfiguration.MaxRate);
            });
    }

    private static string FilmErrorMessage(int id) =>
        $"There is no film with id = {id}";
}