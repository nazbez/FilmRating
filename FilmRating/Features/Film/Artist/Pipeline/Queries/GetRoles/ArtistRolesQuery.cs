using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film.Artist;

public record ArtistRolesQuery : IRequest<IEnumerable<ArtistRoleVm>>
{
    [UsedImplicitly]
    public class ArtistRolesQueryHandler : IRequestHandler<ArtistRolesQuery, IEnumerable<ArtistRoleVm>>
    {
        private readonly IRepository<ArtistRoleEntity, int> artistRoleRepository;
        private readonly IMapper mapper;

        public ArtistRolesQueryHandler(IRepository<ArtistRoleEntity, int> artistRoleRepository, IMapper mapper)
        {
            this.artistRoleRepository = artistRoleRepository;
            this.mapper = mapper;
        }

        public Task<IEnumerable<ArtistRoleVm>> Handle(ArtistRolesQuery request, CancellationToken cancellationToken)
        {
            var artists = artistRoleRepository.Get();

            var artistVms = mapper.Map<IEnumerable<ArtistRoleVm>>(artists);
            
            return Task.FromResult(artistVms);
        }
    }
}