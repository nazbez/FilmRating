using FilmRating.Features.Film.Rating.Models;
using FilmRating.Features.Film.Rating.Persistence;
using FilmRating.Infrastructure.Repository;
using JetBrains.Annotations;
using MapsterMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film.Rating.Pipeline.Commands;

public record RatingCreateCommand(
    int FilmId,
    string UserId,
    int Rate) : IRequest<RatingVm>
{
    [UsedImplicitly]
    public class RatingCreateCommandHandler : IRequestHandler<RatingCreateCommand, RatingVm>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public RatingCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<RatingVm> Handle(RatingCreateCommand request, CancellationToken cancellationToken)
        {
            var rating = RatingEntity.Create(request.FilmId, request.UserId, request.Rate);

            unitOfWork.Repository<RatingEntity, int>().Add(rating);

            await unitOfWork.CompleteAsync(cancellationToken);

            var ratingVm = mapper.Map<RatingVm>(rating);

            return ratingVm;
        }
    }
}