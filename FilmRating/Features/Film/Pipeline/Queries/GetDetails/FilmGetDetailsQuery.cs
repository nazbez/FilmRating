using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film.GetDetails;

public record FilmGetDetailsQuery(int Id) : IRequest<FilmDetailsVm>
{
    [UsedImplicitly]
    public class FilmGetAllQueryHandler : IRequestHandler<FilmGetDetailsQuery, FilmDetailsVm>
    {
        private readonly IRepository<FilmEntity, int> filmRepository;
        private readonly IMapper mapper;

        public FilmGetAllQueryHandler(
            IRepository<FilmEntity, int> filmRepository,
            IMapper mapper)
        {
            this.filmRepository = filmRepository;
            this.mapper = mapper;
        }

        public Task<FilmDetailsVm> Handle(FilmGetDetailsQuery request, CancellationToken cancellationToken)
        {
            var film = filmRepository.Find(
                new FilmGetByIdSpecification(request.Id, true, true, true)).First();

            var filmVm = mapper.Map<FilmDetailsVm>(film);

            return Task.FromResult(filmVm);
        }
    }
}
