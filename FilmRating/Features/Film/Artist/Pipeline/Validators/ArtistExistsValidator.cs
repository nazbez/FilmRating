using FilmRating.Infrastructure.Repository;
using FluentValidation;

namespace FilmRating.Features.Film.Artist;

public class ArtistExistsValidator : AbstractValidator<Guid>
{
    private static string ErrorMessage(Guid id) => $"Artist with id = {id} does not exist";
    
    public ArtistExistsValidator(IRepository<ArtistEntity, Guid> artistRepository)
    {
        RuleFor(id => id)
            .Must(id => artistRepository.FindById(id) != null)
            .WithMessage(ErrorMessage);
    }
}