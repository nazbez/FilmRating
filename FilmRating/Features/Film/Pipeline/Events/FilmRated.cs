using FilmRating.Features.Film.Rating;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film;

public record FilmRated(int FilmId) : INotification;

[UsedImplicitly]
public class FilmRatedHandler : INotificationHandler<FilmRated>
{
    private readonly IUnitOfWork unitOfWork;

    public FilmRatedHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task Handle(FilmRated notification, CancellationToken cancellationToken)
    {
        var film = unitOfWork.Repository<FilmEntity, int>().FindById(notification.FilmId)!;
        var filmRatings = unitOfWork.Repository<RatingEntity, int>()
            .Find(new RatingGetByFilmId(notification.FilmId))
            .Select(x => x.Rate!.Value);
            
        film.UpdateRating(filmRatings);
        unitOfWork.Repository<FilmEntity, int>().Update(film);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}