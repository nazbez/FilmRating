using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Artist;

public record ArtistDeleteCommand(Guid Id) : IRequest<Unit>
{
    [UsedImplicitly]
    public class ArtistDeleteCommandHandler : IRequestHandler<ArtistDeleteCommand, Unit>
    {
        private readonly IRepository<ArtistEntity, Guid> artistRepository;

        public ArtistDeleteCommandHandler(IRepository<ArtistEntity, Guid> artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        public Task<Unit> Handle(ArtistDeleteCommand request, CancellationToken cancellationToken)
        {
            var artist = artistRepository.FindById(request.Id);

            if (artist is not null)
            {
                artistRepository.Remove(artist);
            }
            
            return Task.FromResult(Unit.Value);
        }
    }
}