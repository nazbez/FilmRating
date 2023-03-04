using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Artist;

public record ArtistDeleteCommand(Guid Id) : IRequest
{
    [UsedImplicitly]
    public class ArtistDeleteCommandHandler : IRequestHandler<ArtistDeleteCommand>
    {
        private readonly IRepository<ArtistEntity, Guid> artistRepository;

        public ArtistDeleteCommandHandler(IRepository<ArtistEntity, Guid> artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        public Task Handle(ArtistDeleteCommand request, CancellationToken cancellationToken)
        {
            var artist = artistRepository.FindById(request.Id);

            if (artist is not null)
            {
                artistRepository.Remove(artist);
            }
            
            return Task.CompletedTask;
        }
    }
}