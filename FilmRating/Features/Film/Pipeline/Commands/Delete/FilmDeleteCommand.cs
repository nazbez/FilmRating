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

        public FilmDeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(FilmDeleteCommand request, CancellationToken cancellationToken)
        {
            var film = unitOfWork.Repository<FilmEntity, int>()
                .FindById(request.Id);

            if (film != null)
            {
                unitOfWork.Repository<FilmEntity, int>().Remove(film);
                await unitOfWork.CompleteAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}