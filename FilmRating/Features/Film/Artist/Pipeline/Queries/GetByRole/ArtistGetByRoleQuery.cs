using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film.Artist.GetByRole;

public record ArtistGetByRoleQuery(int RoleId) : IRequest<IEnumerable<ArtistVm>>;

[UsedImplicitly]
public class ArtistGetByRoleQueryHandler : IRequestHandler<ArtistGetByRoleQuery, IEnumerable<ArtistVm>>
{
    private readonly IRepository<ArtistEntity, Guid> artistRepository;
    private readonly IMapper mapper;

    public ArtistGetByRoleQueryHandler(IRepository<ArtistEntity, Guid> artistRepository, IMapper mapper)
    {
        this.artistRepository = artistRepository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<ArtistVm>> Handle(ArtistGetByRoleQuery request, CancellationToken cancellationToken)
    {
        var artists = artistRepository.Find(new ArtistGetByRoleSpecification(request.RoleId));

        var artistVms = mapper.Map<IEnumerable<ArtistVm>>(artists);
            
        return Task.FromResult(artistVms);
    }
}