using FilmRating.Features.Film.Artist.GetByRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FilmRating.Features.Film.Artist.ArtistRoleEntityConstants;
using static FilmRating.Features.Authentication.UserRoleEntityConstants;

namespace FilmRating.Features.Film.Artist;

[ApiController]
[Authorize(Roles = Administrator)]
[Route("api/[controller]")]
public class ArtistController : Controller
{
    private readonly IMediator mediator;

    public ArtistController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ArtistCreateModel model)
    {
        var command = new ArtistCreateCommand(model.FirstName, model.LastName, model.RoleIds);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ArtistUpdateModel model)
    {
        var command = new ArtistUpdateCommand(id, model);
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new ArtistDeleteCommand(id);
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
        var query = new ArtistGetAllQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("Directors")]
    public async Task<IActionResult> Directors()
    {
        var query = new ArtistGetByRoleQuery(DirectorId);
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("Actors")]
    public async Task<IActionResult> Actors()
    {
        var query = new ArtistGetByRoleQuery(ActorId);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("Roles")]
    public async Task<IActionResult> Roles()
    {
        var query = new ArtistRolesQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}