using FilmRating.Infrastructure.Repository;
using FluentValidation;

namespace FilmRating.Features.Film.Rating;

public record RatingIsFavoriteValidatorModel(int FilmId);

public class RatingIsFavoriteValidator : AbstractValidator<RatingIsFavoriteValidatorModel>
{
    public RatingIsFavoriteValidator(
        IRepository<FilmEntity, int> filmRepository)
    {
        RuleFor(r => r.FilmId)
            .Must(id => filmRepository.Contains(x => x.Id == id))
            .WithMessage(r => FilmErrorMessage(r.FilmId));
    }

    private static string FilmErrorMessage(int id) =>
        $"There is no film with id = {id}";
}