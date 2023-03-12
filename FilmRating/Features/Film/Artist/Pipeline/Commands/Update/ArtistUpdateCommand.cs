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
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ArtistUpdateCommandHandler(
            IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ArtistVm> Handle(ArtistUpdateCommand request, CancellationToken cancellationToken)
        {
            var (firstName, lastName, roleIds) = request.Model;
            
            var roles = unitOfWork.Repository<ArtistRoleEntity, int>()
                .Find(new ArtistRoleGetByIdsSpecification(roleIds))
                .ToList();

            var artist = unitOfWork.Repository<ArtistEntity, Guid>()
                .Find(new ArtistGetByIdSpecification(request.Id, true))
                .FirstOrDefault();

            var updatedArtist = UpdateArtist(artist!, firstName, lastName, roles);
            
            unitOfWork.Repository<ArtistEntity, Guid>().Update(updatedArtist);

            await unitOfWork.CompleteAsync(cancellationToken);

            var artistVm = mapper.Map<ArtistVm>(updatedArtist);

            return artistVm;
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