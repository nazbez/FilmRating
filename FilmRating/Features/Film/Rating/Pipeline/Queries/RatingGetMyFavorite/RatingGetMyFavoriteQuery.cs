using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record RatingGetMyFavoriteQuery() : IRequest<IEnumerable<RatingUserMyFavorites>>
{
    [UsedImplicitly]
    public class RatingGetMyFavoriteQueryHandler : IRequestHandler<RatingGetMyFavoriteQuery, IEnumerable<RatingUserMyFavorites>>
    {
        private readonly IRepository<RatingEntity, int> repository;
        private readonly IUserProvider userProvider;
        private readonly IMapper mapper;

        public RatingGetMyFavoriteQueryHandler(IRepository<RatingEntity, int> repository, IUserProvider userProvider, IMapper mapper)
        {
            this.repository = repository;
            this.userProvider = userProvider;
            this.mapper = mapper;
        }

        public Task<IEnumerable<RatingUserMyFavorites>> Handle(RatingGetMyFavoriteQuery request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId()!;

            var rates = repository.Find(new RatingGetByUserId(userId)).Where(e => e.IsFavourite);
            
            var artistVms = mapper.Map<IEnumerable<RatingUserMyFavorites>>(rates);

            return Task.FromResult(artistVms);
        }
    }
}