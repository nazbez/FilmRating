using FilmRating.Infrastructure.Repository;
using FluentValidation;

namespace FilmRating.Features.Film.Artist;

public class ArtistExistsValidator : AbstractValidator<Guid>
{
    public ArtistExistsValidator(IRepository<ArtistEntity, Guid> artistRepository)
    {
        RuleFor(id => id)
            .Must(id => artistRepository.Contains(x => x.Id == id))
            .WithMessage(ErrorMessage);
    }
    
    private static string ErrorMessage(Guid id) =>
        $"Artist with id = {id} does not exist";
}