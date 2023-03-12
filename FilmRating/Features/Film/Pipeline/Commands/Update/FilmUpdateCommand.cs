using FilmRating.Features.Film.Artist;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film;

public record FilmUpdateCommand(int Id, FilmUpdateModel Model) : IRequest<FilmVm>
{
    [UsedImplicitly]
    public class FilmUpdateCommandHandler : IRequestHandler<FilmUpdateCommand, FilmVm>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        
        public FilmUpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        
        public async Task<FilmVm> Handle(FilmUpdateCommand request, CancellationToken cancellationToken)
        {
            var actors = unitOfWork.Repository<ArtistEntity, Guid>()
                .Find(new ArtistGetByIdsSpecification(request.Model.ActorIds))
                .ToList();

            var film = unitOfWork.Repository<FilmEntity, int>()
                .Find(new FilmGetByIdSpecification(request.Id, withActors: true))
                .First();

            var updatedFilm = UpdateFilm(film, request.Model, actors);
            
            unitOfWork.Repository<FilmEntity, int>().Update(updatedFilm);

            await unitOfWork.CompleteAsync(cancellationToken);

            var filmVm = mapper.Map<FilmVm>(film);
            
            return filmVm;
        }

        private static FilmEntity UpdateFilm(FilmEntity film, FilmUpdateModel model, IList<ArtistEntity> actors)
        {
            film.UpdateTitle(model.Title);
            film.UpdateYear(model.Year);
            film.UpdateShortDescription(model.ShortDescription);
            film.UpdateBudget(model.Budget);
            film.UpdateDuration(TimeSpan.FromMinutes(model.DurationInMinutes));
            film.UpdateGenreId(model.GenreId);
            film.UpdateDirectorId(model.DirectorId);
            film.UpdateActors(actors);

            return film;
        }
    }
}