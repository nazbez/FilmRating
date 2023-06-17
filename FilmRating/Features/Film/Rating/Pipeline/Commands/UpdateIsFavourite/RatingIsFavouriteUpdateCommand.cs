using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record RatingIsFavouriteUpdateCommand(int FilmId, bool IsFavorite) : IRequest<Unit>
{
    [UsedImplicitly]
    public class RatingIsFavoriteUpdateCommandHandler : IRequestHandler<RatingIsFavouriteUpdateCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider userProvider;

        public RatingIsFavoriteUpdateCommandHandler(
            IUnitOfWork unitOfWork, 
            IUserProvider userProvider, 
            IPublisher publisher)
        {
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
        }

        public async Task<Unit> Handle(RatingIsFavouriteUpdateCommand request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId()!;
            
            var rating = unitOfWork.Repository<RatingEntity, int>()
                .Find(new RatingGetByUserIdAndFilmId(request.FilmId, userId))
                .FirstOrDefault();

            if (rating is null)
            {
                rating = RatingEntity.Create(request.FilmId, userId, null, request.IsFavorite);
                unitOfWork.Repository<RatingEntity, int>().Add(rating);
            }
            else
            {
                rating.UpdateIsFavorite(request.IsFavorite);
                unitOfWork.Repository<RatingEntity, int>().Update(rating);
            }

            await unitOfWork.CompleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}