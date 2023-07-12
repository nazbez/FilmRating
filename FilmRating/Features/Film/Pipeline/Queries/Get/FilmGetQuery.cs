using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film;

public record FilmGetQuery(int Id) : IRequest<FilmDetailsVm>;

[UsedImplicitly]
public class FilmGetQueryHandler : IRequestHandler<FilmGetQuery, FilmDetailsVm>
{
    private readonly IRepository<FilmEntity, int> filmRepository;
    private readonly IMapper mapper;

    public FilmGetQueryHandler(
        IRepository<FilmEntity, int> filmRepository,
        IMapper mapper)
    {
        this.filmRepository = filmRepository;
        this.mapper = mapper;
    }

    public Task<FilmDetailsVm> Handle(FilmGetQuery request, CancellationToken cancellationToken)
    {
        var film = filmRepository.Find(
            new FilmGetByIdSpecification(request.Id, true, true, true)).First();

        var filmVm = mapper.Map<FilmDetailsVm>(film);

        return Task.FromResult(filmVm);
    }
}