using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record RatingUpdateCommand(int FilmId, int Rate) : IRequest<Unit>
{
    [UsedImplicitly]
    public class RatingUpdateCommandHandler : IRequestHandler<RatingUpdateCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider userProvider;
        private readonly IPublisher publisher;

        public RatingUpdateCommandHandler(
            IUnitOfWork unitOfWork, 
            IUserProvider userProvider, 
            IPublisher publisher)
        {
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
            this.publisher = publisher;
        }

        public async Task<Unit> Handle(RatingUpdateCommand request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId()!;
            
            var rating = unitOfWork.Repository<RatingEntity, int>()
                .Find(new RatingGetByUserIdAndFilmId(request.FilmId, userId))
                .FirstOrDefault()!;
            
            rating.UpdateRate(request.Rate);
            
            unitOfWork.Repository<RatingEntity, int>().Update(rating);

            await unitOfWork.CompleteAsync(cancellationToken);

            await publisher.Publish(new FilmRatedEvent(request.FilmId), cancellationToken);

            return Unit.Value;
        }
    }
}