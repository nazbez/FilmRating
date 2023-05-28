using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film.Rating;

public record RatingGetOptionsQuery : IRequest<IEnumerable<int>>
{
    [UsedImplicitly]
    public class RatingGetOptionsQueryHandler : IRequestHandler<RatingGetOptionsQuery, IEnumerable<int>>
    {
        private readonly RatingConfiguration ratingConfiguration;

        public RatingGetOptionsQueryHandler(RatingConfiguration ratingConfiguration)
        {
            this.ratingConfiguration = ratingConfiguration;
        }

        public Task<IEnumerable<int>> Handle(RatingGetOptionsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                Enumerable.Range(ratingConfiguration.MinRate, ratingConfiguration.MaxRate + 1));
        }
    }
}