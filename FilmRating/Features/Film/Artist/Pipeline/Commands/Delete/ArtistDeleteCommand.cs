using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Artist;

public record ArtistDeleteCommand(Guid Id) : IRequest<Unit>;

[UsedImplicitly]
public class ArtistDeleteCommandHandler : IRequestHandler<ArtistDeleteCommand, Unit>
{
    private readonly IUnitOfWork unitOfWork;

    public ArtistDeleteCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ArtistDeleteCommand request, CancellationToken cancellationToken)
    {
        var artist = unitOfWork.Repository<ArtistEntity, Guid>().FindById(request.Id);

        if (artist is not null)
        {
            unitOfWork.Repository<ArtistEntity, Guid>().Remove(artist);
            await unitOfWork.CompleteAsync(cancellationToken);
        }
            
        return Unit.Value;
    }
}