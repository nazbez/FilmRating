// TODO: Check async problems
/*using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace FilmRating.Features.Film.Rating.Pipeline.Validators;

public record RatingValidatorModel(
    int FilmId,
    string UserId,
    int Rate);

public class RatingValidator : AbstractValidator<RatingValidatorModel>
{
    public RatingValidator(
        IRepository<FilmEntity, int> filmRepository,
        UserManager<User> userManager,
        RatingConfiguration ratingConfiguration)
    {
        RuleFor(r => r.FilmId)
            .Must(id =>
            {
                var film = filmRepository.FindById(id);
                return film != null;
            })
            .WithMessage(r => FilmErrorMessage(r.FilmId))
            .DependentRules(() =>
            {
                RuleFor(r => r.UserId)
                    .MustAsync(async (id, _) =>
                    {
                        var user = await userManager.FindByIdAsync(id.ToString());
                        return user != null;
                    })
                    .WithMessage(r => UserErrorMessage(r.UserId));
            })
            .DependentRules(() =>
            {
                RuleFor(r => r.Rate)
                    .GreaterThanOrEqualTo(ratingConfiguration.MinRate)
                    .LessThanOrEqualTo(ratingConfiguration.MaxRate);
            });
    }

    private static string FilmErrorMessage(int id) =>
        $"There is no film with id = {id}";

    private static string UserErrorMessage(string id) =>
        $"There is no user with id = {id}";
}*/