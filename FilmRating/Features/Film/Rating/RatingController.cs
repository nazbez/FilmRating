using FilmRating.Features.Film.Artist;
using FilmRating.Features.Film.Rating.Models;
using FilmRating.Features.Film.Rating.Pipeline.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmRating.Features.Film.Rating;

[ApiController]
/*[Authorize(Roles = "Critic")]*/
[Route("api/[controller]")]
public class RatingController : Controller
{

    private readonly IMediator mediator;

    public RatingController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    // TODO: remove userId string
    [HttpPost]
    public async Task<IActionResult> Create(RatingCreateModel model)
    {
        var command = new RatingCreateCommand(model.FilmId, "a7d892c7-62d2-4e1e-bdce-92ac5589ea11", model.Rate);
        var result = await mediator.Send(command);
        return Ok(result);
    }
}