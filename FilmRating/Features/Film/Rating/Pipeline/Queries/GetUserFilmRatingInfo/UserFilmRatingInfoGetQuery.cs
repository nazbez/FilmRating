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

            var result = GetUserFilmRatingInfo(request, rate);

            return Task.FromResult(result);
        }

        private UserFilmRatingInfo GetUserFilmRatingInfo(UserFilmRatingInfoGetQuery request, RatingEntity? rate)
        {
            if (rate is null)
                return new UserFilmRatingInfo(false, null, request.FilmId);
            else if (rate.Rate is null)
                return new UserFilmRatingInfo(false, null, rate.FilmId, rate.IsFavourite);
            else 
                return new UserFilmRatingInfo(true, rate.Rate, request.FilmId, rate.IsFavourite);
        }
    }
}