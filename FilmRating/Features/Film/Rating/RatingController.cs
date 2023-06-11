using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FilmRating.Features.Authentication.UserRoleEntityConstants;

namespace FilmRating.Features.Film.Rating;

[ApiController]
[Authorize(Roles = Critic)]
[Route("api/[controller]")]
public class RatingController : Controller
{
    private readonly IMediator mediator;

    public RatingController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(RatingCreateModel model)
    {
        var command = new RatingCreateCommand(model.FilmId, model.Rate);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(RatingUpdateModel model)
    {
        var command = new RatingUpdateCommand(model.FilmId, model.Rate);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("Film/{filmId:int}/My")]
    public async Task<IActionResult> GetUserFilmRate(int filmId)
    {
        var query = new RatingGetUserRateQuery(filmId);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("Options")]
    public async Task<IActionResult> GetOptions()
    {
        var query = new RatingGetOptionsQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("IsFavourite/All")]
    public async Task<IActionResult> GetMyFavourite()
    {
        var query = new RatingGetMyFavoriteQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("IsFavourite/{filmId:int}/My")]
    public async Task<IActionResult> GetIsFavourite(int filmId)
    {
        var query = new RatingGetUserIsFavouriteQuery(filmId);
        var result = await mediator.Send(query);
        return Ok(result);
    }
    [HttpPost("IsFavourite")]
    public async Task<IActionResult> CreateIsFavorite(RatingIsFavoriteCreateModel model)
    {
        var command = new RatingIsFavoriteCreateCommand(model.FilmId, model.IsFavorite);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("IsFavourite")]
    public async Task<IActionResult> UpdateIsFavorite(RatingisFavoriteUpdateModel model)
    {
        var command = new RatingIsFavoriteUpdateCommand(model.FilmId, model.IsFavorite);
        var result = await mediator.Send(command);
        return Ok(result);
    }
}