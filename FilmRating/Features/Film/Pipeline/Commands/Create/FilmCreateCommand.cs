using System.ComponentModel.DataAnnotations;
using FilmRating.Features.Film.Artist;
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
    IEnumerable<Guid> ActorIds) : IRequest<FilmVm>
{ 
    [UsedImplicitly]
    public class FilmCreateCommandHandler : IRequestHandler<FilmCreateCommand, FilmVm>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public FilmCreateCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<FilmVm> Handle(FilmCreateCommand request, CancellationToken cancellationToken)
        {
            var actors = unitOfWork.Repository<ArtistEntity, Guid>()
                .Find(new ArtistGetByIdsSpecification(request.ActorIds))
                .ToList();

            var film = FilmEntity.Create(
                request.Title,
                request.Year,
                request.ShortDescription,
                request.Budget,
                TimeSpan.FromMinutes(request.DurationInMinutes),
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