using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmRating.Features.Film.Artist;

[ApiController]
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
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new ArtistDeleteCommand(id);
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var query = new ArtistGetAllQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("Roles")]
    public async Task<IActionResult> GetRoles()
    {
        var query = new ArtistRolesQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}