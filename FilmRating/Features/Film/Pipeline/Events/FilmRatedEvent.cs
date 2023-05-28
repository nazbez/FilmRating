using FilmRating.Features.Film.Rating;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film;

public record FilmRatedEvent(int FilmId) : INotification
{
    [UsedImplicitly]
    public class FilmRatedEventHandler : INotificationHandler<FilmRatedEvent>
    {
        private readonly IUnitOfWork unitOfWork;

        public FilmRatedEventHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(FilmRatedEvent notification, CancellationToken cancellationToken)
        {
            var film = unitOfWork.Repository<FilmEntity, int>().FindById(notification.FilmId)!;
            var filmRatings = unitOfWork.Repository<RatingEntity, int>()
                .Find(new RatingGetByFilmId(notification.FilmId))
                .Select(x => x.Rate);
            
            film.UpdateRating(filmRatings);
            unitOfWork.Repository<FilmEntity, int>().Update(film);
            await unitOfWork.CompleteAsync(cancellationToken);
        }
    }
}