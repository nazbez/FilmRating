using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record RatingIsFavoriteUpdateCommand(int FilmId, bool IsFavorite) : IRequest<Unit>
{
    [UsedImplicitly]
    public class RatingIsFavoriteUpdateCommandHandler : IRequestHandler<RatingIsFavoriteUpdateCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider userProvider;
        private readonly IPublisher publisher;

        public RatingIsFavoriteUpdateCommandHandler(
            IUnitOfWork unitOfWork, 
            IUserProvider userProvider, 
            IPublisher publisher)
        {
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
            this.publisher = publisher;
        }

        public async Task<Unit> Handle(RatingIsFavoriteUpdateCommand request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId()!;
            
            var rating = unitOfWork.Repository<RatingEntity, int>()
                .Find(new RatingGetByUserIdAndFilmId(request.FilmId, userId))
                .FirstOrDefault()!;
            
            rating.UpdateIsFavorite(request.IsFavorite);
            
            unitOfWork.Repository<RatingEntity, int>().Update(rating);

            await unitOfWork.CompleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}