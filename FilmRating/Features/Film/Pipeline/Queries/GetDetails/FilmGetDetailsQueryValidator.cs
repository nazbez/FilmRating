using FilmRating.Infrastructure.Repository;
using FluentValidation;

namespace FilmRating.Features.Film.GetDetails;

public class FilmGetDetailsQueryValidator : AbstractValidator<FilmGetDetailsQuery>
{
    public FilmGetDetailsQueryValidator(IRepository<FilmEntity, int> filmRepository)
    {
        RuleFor(q => q)
            .Must(q => filmRepository.Contains(x => x.Id == q.Id))
            .WithMessage(ErrorMessage);
    }

    private static string ErrorMessage(FilmGetDetailsQuery q) =>
        $"Film with id = {q.Id} does not exist";
}