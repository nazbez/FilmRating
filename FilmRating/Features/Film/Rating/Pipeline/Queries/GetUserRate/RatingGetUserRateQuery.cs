using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record RatingGetUserRateQuery(int FilmId) : IRequest<RatingUserRate>
{
    [UsedImplicitly]
    public class RatingGetUserRateQueryHandler : IRequestHandler<RatingGetUserRateQuery, RatingUserRate>
    {
        private readonly IRepository<RatingEntity, int> repository;
        private readonly IUserProvider userProvider;

        public RatingGetUserRateQueryHandler(IRepository<RatingEntity, int> repository, IUserProvider userProvider)
        {
            this.repository = repository;
            this.userProvider = userProvider;
        }

        public Task<RatingUserRate> Handle(RatingGetUserRateQuery request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId()!;

            var rate = repository.Find(new RatingGetByUserIdAndFilmId(request.FilmId, userId))
                .FirstOrDefault();

            var result = rate == null 
                ? new RatingUserRate(false, null, request.FilmId)
                : new RatingUserRate(true, rate.Rate, request.FilmId);

            return Task.FromResult(result);
        }
    }
}