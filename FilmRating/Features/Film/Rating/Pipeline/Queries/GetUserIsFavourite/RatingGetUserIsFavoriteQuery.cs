using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record RatingGetUserIsFavouriteQuery(int FilmId) : IRequest<RatingUserIsFavorite>
{
    [UsedImplicitly]
    public class RatingGetUserIsFavouriteQueryHandler : IRequestHandler<RatingGetUserIsFavouriteQuery, RatingUserIsFavorite>
    {
        private readonly IRepository<RatingEntity, int> repository;
        private readonly IUserProvider userProvider;

        public RatingGetUserIsFavouriteQueryHandler(IRepository<RatingEntity, int> repository, IUserProvider userProvider)
        {
            this.repository = repository;
            this.userProvider = userProvider;
        }

        public Task<RatingUserIsFavorite> Handle(RatingGetUserIsFavouriteQuery request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId()!;

            var rate = repository.Find(new RatingGetByUserIdAndFilmId(request.FilmId, userId))
                .FirstOrDefault();

            var result = rate == null 
                ? new RatingUserIsFavorite(false, false, request.FilmId)
                : new RatingUserIsFavorite(true, rate.IsFavourite, request.FilmId);

            return Task.FromResult(result);
        }
    }
}