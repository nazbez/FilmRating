using System.ComponentModel.DataAnnotations;
using FilmRating.Features.Film.Artist;
using FilmRating.Infrastructure.AzureStorage;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;

namespace FilmRating.Features.Film;

public record FilmCreateCommand(
    [Required] string Title,
    int Year,
    [Required] string ShortDescription,
    decimal Budget,
    int DurationInMinutes,
    int GenreId,
    Guid DirectorId,
    IEnumerable<Guid> ActorIds,
    [Required] IFormFile Photo) : IRequest<FilmVm>
{
    [UsedImplicitly]
    public class FilmCreateCommandHandler : IRequestHandler<FilmCreateCommand, FilmVm>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAzureStorageService azureStorageService;

        public FilmCreateCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork, 
            IAzureStorageService azureStorageService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.azureStorageService = azureStorageService;
        }

        public async Task<FilmVm> Handle(FilmCreateCommand request, CancellationToken cancellationToken)
        {
            var actors = unitOfWork.Repository<ArtistEntity, Guid>()
                .Find(new ArtistGetByIdsSpecification(request.ActorIds))
                .ToList();

            var blobResult = await azureStorageService.Upload(request.Photo);

            var photoPath = blobResult.Error ? string.Empty : blobResult.Blob.Uri;

            var film = FilmEntity.Create(
                request.Title,
                request.Year,
                request.ShortDescription,
                request.Budget,
                TimeSpan.FromMinutes(request.DurationInMinutes),
                photoPath!,
                request.GenreId,
                request.DirectorId,
                actors);
            
            unitOfWork.Repository<FilmEntity, int>().Add(film);

            await unitOfWork.CompleteAsync(cancellationToken);

            var filmVm = mapper.Map<FilmVm>(film);
            return filmVm;
        }
    }
}