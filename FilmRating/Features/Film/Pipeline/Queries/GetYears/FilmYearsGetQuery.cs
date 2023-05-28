using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film;

public record FilmYearsGetQuery : IRequest<List<int>>
{
    [UsedImplicitly]
    public class FilmYearsGetQueryHandler : IRequestHandler<FilmYearsGetQuery, List<int>>
    {
        private readonly FilmConfiguration filmConfiguration;

        public FilmYearsGetQueryHandler(FilmConfiguration filmConfiguration)
        {
            this.filmConfiguration = filmConfiguration;
        }

        public Task<List<int>> Handle(FilmYearsGetQuery request, CancellationToken cancellationToken)
        {
            var years = Enumerable.Range(
                filmConfiguration.MinimumYear,
                DateTimeOffset.Now.Year - filmConfiguration.MinimumYear + 1)
                .OrderDescending()
                .ToList();

            return Task.FromResult(years);
        }
    }
}