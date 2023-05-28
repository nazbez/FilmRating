using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film;

public record FilmGetRatingQuery(int Id) : IRequest<double>
{
    [UsedImplicitly]
    public class FilmGetRatingQueryHandler : IRequestHandler<FilmGetRatingQuery, double>
    {
        private readonly IRepository<FilmEntity, int> filmRepository;

        public FilmGetRatingQueryHandler(IRepository<FilmEntity, int> filmRepository)
        {
            this.filmRepository = filmRepository;
        }

        public Task<double> Handle(FilmGetRatingQuery request, CancellationToken cancellationToken)
        {
            var film = filmRepository.FindById(request.Id)!;

            return Task.FromResult(film.Rating);
        }
    }
}