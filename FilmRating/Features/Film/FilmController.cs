using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FilmRating.Features.Authentication.UserRoleEntityConstants;

namespace FilmRating.Features.Film;

[ApiController]
[Route("api/[controller]")]
public class FilmController : Controller
{
    private readonly IMediator mediator;

    public FilmController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = Administrator)]
    public async Task<IActionResult> Create([FromForm] FilmCreateCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = Administrator)]
    public async Task<IActionResult> Update(int id, [FromForm] FilmUpdateModel model)
    {
        var command = new FilmUpdateCommand(id, model);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = Administrator)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new FilmDeleteCommand(id);
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var query = new FilmGetAllQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var query = new FilmGetQuery(id);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:int}/Rating")]
    public async Task<IActionResult> GetRating(int id)
    {
        var query = new FilmGetRatingQuery(id);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("Years")]
    public async Task<IActionResult> GetYears()
    {
        var query = new FilmYearsGetQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("IsFavourite/All")]
    public async Task<IActionResult> GetMyFavourite()
    {
        var query = new FilmGetMyFavoriteQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search(FilmSearchQuery filmSearchQuery)
    {
        var result = await mediator.Send(filmSearchQuery);
        return Ok(result);
    }
}
