using FilmRating.Features.Authentication;
using FilmRating.Features.Film.Rating;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film;

public record FilmGetMyFavoriteQuery : IRequest<IEnumerable<FilmVm>>
{
    [UsedImplicitly]
    public class GetMyFavoriteQueryHandler : IRequestHandler<FilmGetMyFavoriteQuery, IEnumerable<FilmVm>>
    {
        private readonly IRepository<RatingEntity, int> ratingRepository;
        private readonly IRepository<FilmEntity, int> filmRepository;
        private readonly IUserProvider userProvider;
        private readonly IMapper mapper;

        public GetMyFavoriteQueryHandler(
            IRepository<RatingEntity, int> ratingRepository,
            IRepository<FilmEntity, int> filmRepository,
            IUserProvider userProvider,
            IMapper mapper)
        {
            this.ratingRepository = ratingRepository;
            this.filmRepository = filmRepository;
            this.userProvider = userProvider;
            this.mapper = mapper;
        }

        public Task<IEnumerable<FilmVm>> Handle(FilmGetMyFavoriteQuery request, CancellationToken cancellationToken)
        {
            var userId = userProvider.GetUserId()!;

            var filmIds = ratingRepository.Find(new RatingGetFavoriteByUserId(userId))
                .Select(x => x.FilmId);
            
            var films = filmRepository.Find(
                new FilmsGetByIdsSpecification(filmIds, true));
            
            var filmVms = mapper.Map<IEnumerable<FilmVm>>(films);

            return Task.FromResult(filmVms);
        }
    }
}