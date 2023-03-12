using FilmRating.Infrastructure.Repository;
using FluentValidation;

namespace FilmRating.Features.Film;

public class FilmExistsValidator : AbstractValidator<int>
{
    public FilmExistsValidator(IRepository<FilmEntity, int> filmRepository)
    {
        RuleFor(id => id)
            .Must(id => filmRepository.Contains(x => x.Id == id))
            .WithMessage(ErrorMessage);
    }
    
    private static string ErrorMessage(int id) =>
        $"Film with id = {id} does not exist";
}