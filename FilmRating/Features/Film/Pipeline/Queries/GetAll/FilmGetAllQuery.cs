using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film;

public class FilmGetAllQuery : IRequest<IEnumerable<FilmVm>> {}

[UsedImplicitly]
public class FilmGetAllQueryHandler : IRequestHandler<FilmGetAllQuery, IEnumerable<FilmVm>>
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
        
    public Task<IEnumerable<FilmVm>> Handle(FilmGetAllQuery request, CancellationToken cancellationToken)
    {
        var films = filmRepository.Find(
            new FilmGetAllSpecification(true, true, true));

        var filmVms = mapper.Map<IEnumerable<FilmVm>>(films);

        return Task.FromResult(filmVms);
    }
}