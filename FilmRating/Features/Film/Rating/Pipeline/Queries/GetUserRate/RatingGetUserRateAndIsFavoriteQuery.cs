using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record RatingGetUserRateAndIsFavoriteQuery(int FilmId) : IRequest<RatingUserRateAndIsFavorite>
{
    [UsedImplicitly]
    public class RatingGetUserRateAndIsFavoriteQueryHandler : IRequestHandler<RatingGetUserRateAndIsFavoriteQuery, RatingUserRateAndIsFavorite>
    {
        private readonly IRepository<RatingEntity, int> repository;
        private readonly IUserProvider userProvider;

        public RatingGetUserRateAndIsFavoriteQueryHandler(IRepository<RatingEntity, int> repository, IUserProvider userProvider)
        {
            this.repository = repository;
            this.userProvider = userProvider;
        }

        public Task<RatingUserRateAndIsFavorite> Handle(RatingGetUserRateAndIsFavoriteQuery request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId()!;

            var rate = repository.Find(new RatingGetByUserIdAndFilmId(request.FilmId, userId))
                .FirstOrDefault();

            var result = rate == null 
                ? new RatingUserRateAndIsFavorite(false, null, request.FilmId)
                : new RatingUserRateAndIsFavorite(true, rate.Rate, request.FilmId, rate.IsFavourite);

            return Task.FromResult(result);
        }
    }
}