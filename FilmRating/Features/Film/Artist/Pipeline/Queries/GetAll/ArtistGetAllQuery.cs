using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film.Artist;

public record ArtistGetAllQuery : IRequest<IEnumerable<ArtistVm>>
{
    [UsedImplicitly]
    public class ArtistGetAllQueryHandler : IRequestHandler<ArtistGetAllQuery, IEnumerable<ArtistVm>>
    {
        private readonly IRepository<ArtistEntity, Guid> artistRepository;
        private readonly IMapper mapper;

        public ArtistGetAllQueryHandler(IRepository<ArtistEntity, Guid> artistRepository, IMapper mapper)
        {
            this.artistRepository = artistRepository;
            this.mapper = mapper;
        }

        public Task<IEnumerable<ArtistVm>> Handle(ArtistGetAllQuery request, CancellationToken cancellationToken)
        {
            var artists = artistRepository.Find(new ArtistGetAllSpecification());

            var artistVms = mapper.Map<IEnumerable<ArtistVm>>(artists);
            
            return Task.FromResult(artistVms);
        }
    }
}