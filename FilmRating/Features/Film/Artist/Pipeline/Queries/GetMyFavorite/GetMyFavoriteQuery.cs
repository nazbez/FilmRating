using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record GetMyFavoriteQuery() : IRequest<IEnumerable<RatingUserMyFavorites>>
{
    [UsedImplicitly]
    public class GetMyFavoriteQueryHandler : IRequestHandler<GetMyFavoriteQuery, IEnumerable<RatingUserMyFavorites>>
    {
        private readonly IRepository<RatingEntity, int> repository;
        private readonly IUserProvider userProvider;
        private readonly IMapper mapper;

        public GetMyFavoriteQueryHandler(IRepository<RatingEntity, int> repository, IUserProvider userProvider, IMapper mapper)
        {
            this.repository = repository;
            this.userProvider = userProvider;
            this.mapper = mapper;
        }

        public Task<IEnumerable<RatingUserMyFavorites>> Handle(GetMyFavoriteQuery request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId()!;

            var rates = repository.Find(new RatingGetFavoriteByUserId(userId));
            
            var artistVms = mapper.Map<IEnumerable<RatingUserMyFavorites>>(rates);

            return Task.FromResult(artistVms);
        }
    }
}