using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film.Artist;

public record ArtistUpdateCommand(Guid Id, ArtistUpdateModel Model) : IRequest<ArtistVm>
{
    [UsedImplicitly]
    public class ArtistUpdateCommandHandler : IRequestHandler<ArtistUpdateCommand, ArtistVm>
    {
        private readonly IRepository<ArtistEntity, Guid> artistRepository;
        private readonly IRepository<ArtistRoleEntity, int> artistRoleRepository;
        private readonly IMapper mapper;

        public ArtistUpdateCommandHandler(
            IRepository<ArtistEntity, Guid> artistRepository, 
            IRepository<ArtistRoleEntity, int> artistRoleRepository,
            IMapper mapper)
        {
            this.artistRepository = artistRepository;
            this.artistRoleRepository = artistRoleRepository;
            this.mapper = mapper;
        }

        public Task<ArtistVm> Handle(ArtistUpdateCommand request, CancellationToken cancellationToken)
        {
            var (firstName, lastName, roleIds) = request.Model;
            
            var roles = artistRoleRepository.Get(r =>
                    roleIds.Any(i => i == r.Id))
                .ToList();

            var artist = artistRepository.Get(
                x => x.Id == request.Id,
                x => x.Roles)
                .FirstOrDefault();

            var updatedArtist = UpdateArtist(artist!, firstName, lastName, roles);
            
            artistRepository.Update(updatedArtist);

            var artistVm = mapper.Map<ArtistVm>(updatedArtist);

            return Task.FromResult(artistVm);
        }

        private static ArtistEntity UpdateArtist(
            ArtistEntity artist,
            string firstName,
            string lastName, 
            ICollection<ArtistRoleEntity> roles)
        {
            artist.UpdateFirstName(firstName);
            artist.UpdateLastName(lastName);
            artist.UpdateRoles(roles);
            return artist;
        }
    }
}