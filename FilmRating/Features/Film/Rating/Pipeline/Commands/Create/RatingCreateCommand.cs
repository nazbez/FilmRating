using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record RatingCreateCommand(int FilmId, int Rate) : IRequest<Unit>
{
    [UsedImplicitly]
    public class RatingCreateCommandHandler : IRequestHandler<RatingCreateCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider userProvider;
        private readonly IPublisher publisher;

        public RatingCreateCommandHandler(
            IUnitOfWork unitOfWork, 
            IUserProvider userProvider,
            IPublisher publisher)
        {
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
            this.publisher = publisher;
        }

        public async Task<Unit> Handle(RatingCreateCommand request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId();
            
            var rating = RatingEntity.Create(request.FilmId, userId!, request.Rate);

            unitOfWork.Repository<RatingEntity, int>().Add(rating);

            await unitOfWork.CompleteAsync(cancellationToken);

            await publisher.Publish(new FilmRatedEvent(request.FilmId), cancellationToken);

            return Unit.Value;
        }
    }
}