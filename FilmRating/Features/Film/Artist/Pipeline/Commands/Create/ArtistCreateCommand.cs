using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace FilmRating.Features.Film.Artist;

public record ArtistCreateCommand(
    string FirstName,
    string LastName, 
    IEnumerable<int> RoleIds) : IRequest<ArtistVm>
{
    [UsedImplicitly]
    public class ArtistCreateCommandHandler : IRequestHandler<ArtistCreateCommand, ArtistVm>
    {
        private readonly IRepository<ArtistEntity, Guid> artistRepository;
        private readonly IRepository<ArtistRoleEntity, int> artistRoleRepository;
        private readonly IMapper mapper;

        public ArtistCreateCommandHandler(
            IRepository<ArtistEntity, Guid> artistRepository,
            IRepository<ArtistRoleEntity, int> artistRoleRepository,
            IMapper mapper)
        {
            this.artistRepository = artistRepository;
            this.artistRoleRepository = artistRoleRepository;
            this.mapper = mapper;
        }

        public Task<ArtistVm> Handle(ArtistCreateCommand request, CancellationToken cancellationToken)
        {
            var roles = artistRoleRepository.Get(r =>
                request.RoleIds.Any(i => i == r.Id))
                .ToList();

            var artist = ArtistEntity.Create(request.FirstName, request.LastName);
            
            artistRepository.Create(artist);

            if (!roles.IsNullOrEmpty())
            {
                artist.UpdateRoles(roles);
                artistRepository.Update(artist);
            }

            var artistVm = mapper.Map<ArtistVm>(artist);

            return Task.FromResult(artistVm);
        }
    }
}