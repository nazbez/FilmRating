using FilmRating.Features.Film.Artist;
using FilmRating.Infrastructure.AzureStorage;
using FilmRating.Infrastructure.Extensions;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace FilmRating.Features.Film;

public record FilmUpdateCommand(int Id, FilmUpdateModel Model) : IRequest<FilmVm>
{
    [UsedImplicitly]
    public class FilmUpdateCommandHandler : IRequestHandler<FilmUpdateCommand, FilmVm>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAzureStorageService azureStorageService;
        
        public FilmUpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IAzureStorageService azureStorageService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.azureStorageService = azureStorageService;
        }
        
        public async Task<FilmVm> Handle(FilmUpdateCommand request, CancellationToken cancellationToken)
        {
            var actors = unitOfWork.Repository<ArtistEntity, Guid>()
                .Find(new ArtistGetByIdsSpecification(request.Model.ActorIds))
                .ToList();

            var film = unitOfWork.Repository<FilmEntity, int>()
                .Find(new FilmGetByIdSpecification(request.Id, withActors: true))
                .First();
            
            var photoPath = string.Empty;

            if (request.Model.Photo != null)
            {
                await azureStorageService.Delete(film.PhotoPath.GetFileName());
                var blobResult = await azureStorageService.Upload(request.Model.Photo);
                photoPath = blobResult.Error ? string.Empty : blobResult.Blob.Uri;
            }

            var updatedFilm = UpdateFilm(film, request.Model, actors, photoPath!);
            
            unitOfWork.Repository<FilmEntity, int>().Update(updatedFilm);

            await unitOfWork.CompleteAsync(cancellationToken);

            var filmVm = mapper.Map<FilmVm>(film);
            return filmVm;
        }

        private static FilmEntity UpdateFilm(FilmEntity film, FilmUpdateModel model, IList<ArtistEntity> actors, string photoPath)
        {
            film.UpdateTitle(model.Title);
            film.UpdateYear(model.Year);
            film.UpdateShortDescription(model.ShortDescription);
            film.UpdateBudget(model.Budget);
            film.UpdateDuration(TimeSpan.FromMinutes(model.DurationInMinutes));
            film.UpdateGenreId(model.GenreId);
            film.UpdateDirectorId(model.DirectorId);
            film.UpdateActors(actors);

            if (!photoPath.IsNullOrEmpty())
            {
                film.UpdatePhotoPath(photoPath);
            }

            return film;
        }
    }
}