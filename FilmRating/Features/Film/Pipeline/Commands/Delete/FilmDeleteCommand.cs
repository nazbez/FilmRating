using FilmRating.Infrastructure.AzureStorage;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Features.Film;

public record FilmDeleteCommand(int Id) : IRequest<Unit>
{
    [UsedImplicitly]
    public class FilmDeleteCommandHandler : IRequestHandler<FilmDeleteCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAzureStorageService azureStorageService;

        public FilmDeleteCommandHandler(IUnitOfWork unitOfWork, IAzureStorageService azureStorageService)
        {
            this.unitOfWork = unitOfWork;
            this.azureStorageService = azureStorageService;
        }

        public async Task<Unit> Handle(FilmDeleteCommand request, CancellationToken cancellationToken)
        {
            var film = unitOfWork.Repository<FilmEntity, int>()
                .FindById(request.Id);

            if (film != null)
            {
                await azureStorageService.Delete(film.PhotoPath);
                unitOfWork.Repository<FilmEntity, int>().Remove(film);
                await unitOfWork.CompleteAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}