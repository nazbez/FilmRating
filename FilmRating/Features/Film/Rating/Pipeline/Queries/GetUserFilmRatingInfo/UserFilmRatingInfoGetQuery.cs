using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record UserFilmRatingInfoGetQuery(int FilmId) : IRequest<UserFilmRatingInfo>
{
    [UsedImplicitly]
    public class UserFilmRatingInfoGetQueryHandler : IRequestHandler<UserFilmRatingInfoGetQuery, UserFilmRatingInfo>
    {
        private readonly IRepository<RatingEntity, int> repository;
        private readonly IUserProvider userProvider;

        public UserFilmRatingInfoGetQueryHandler(IRepository<RatingEntity, int> repository, IUserProvider userProvider)
        {
            this.repository = repository;
            this.userProvider = userProvider;
        }

        public Task<UserFilmRatingInfo> Handle(UserFilmRatingInfoGetQuery request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId()!;

            var rate = repository.Find(new RatingGetByUserIdAndFilmId(request.FilmId, userId))
                .FirstOrDefault();

            var result = rate is null 
                ? new UserFilmRatingInfo(false, null, request.FilmId)
                : new UserFilmRatingInfo(true, rate.Rate, request.FilmId, rate.IsFavourite);

            return Task.FromResult(result);
        }
    }
}