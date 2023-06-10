using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record RatingIsFavoriteCreateCommand(int FilmId, bool IsFavorite) : IRequest<Unit>
{
    [UsedImplicitly]
    public class RatingIsFavoriteCreateCommandHandler : IRequestHandler<RatingIsFavoriteCreateCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider userProvider;
        private readonly IPublisher publisher;
        private readonly IRepository<RatingEntity, int> repository;

        public RatingIsFavoriteCreateCommandHandler(
            IRepository<RatingEntity, int> repository,
            IUnitOfWork unitOfWork, 
            IUserProvider userProvider,
            IPublisher publisher)
        {
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
            this.publisher = publisher;
            this.repository = repository;
        }

        public async Task<Unit> Handle(RatingIsFavoriteCreateCommand request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId();
            
            var rating = RatingEntity.Create(request.FilmId, userId!, null, request.IsFavorite);
            
            unitOfWork.Repository<RatingEntity, int>().Add(rating);

            await unitOfWork.CompleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}