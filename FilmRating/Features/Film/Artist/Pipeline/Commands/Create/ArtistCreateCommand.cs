using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film.Artist;

public record ArtistCreateCommand(
    string FirstName,
    string LastName, 
    IEnumerable<int> RoleIds) : IRequest<ArtistVm>
{
    [UsedImplicitly]
    public class ArtistCreateCommandHandler : IRequestHandler<ArtistCreateCommand, ArtistVm>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ArtistCreateCommandHandler(IMapper  mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ArtistVm> Handle(ArtistCreateCommand request, CancellationToken cancellationToken)
        {
            var roles = unitOfWork.Repository<ArtistRoleEntity, int>().Find(
                    new ArtistRoleGetByIdsSpecification(request.RoleIds))
                .ToList();

            var artist = ArtistEntity.Create(request.FirstName, request.LastName, roles);
            
            unitOfWork.Repository<ArtistEntity, Guid>().Add(artist);

            await unitOfWork.CompleteAsync(cancellationToken);

            var artistVm = mapper.Map<ArtistVm>(artist);

            return artistVm;
        }
    }
}