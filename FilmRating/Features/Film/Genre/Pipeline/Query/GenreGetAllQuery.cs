using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film.Genre;

public class GenreGetAllQuery : IRequest<IEnumerable<GenreVm>> {}

[UsedImplicitly]
public class GenreGetAllQueryHandler : IRequestHandler<GenreGetAllQuery, IEnumerable<GenreVm>>
{
    private readonly IRepository<GenreEntity, int> genreRepository;
    private readonly IMapper mapper;

    public GenreGetAllQueryHandler(IRepository<GenreEntity, int> genreRepository, IMapper mapper)
    {
        this.genreRepository = genreRepository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<GenreVm>> Handle(GenreGetAllQuery request, CancellationToken cancellationToken)
    {
        var genres = genreRepository.Find();

        var genreVms = mapper.Map<IEnumerable<GenreVm>>(genres);

        return Task.FromResult(genreVms);
    }
}