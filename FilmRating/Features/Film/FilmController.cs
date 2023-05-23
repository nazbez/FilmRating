using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmRating.Features.Film;

[ApiController]
// TODO Uncomment it when auth feature will be implemented [Authorize]
[Route("api/[controller]")]
public class FilmController : Controller
{
    private readonly IMediator mediator;

    public FilmController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] FilmCreateCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromForm] FilmUpdateModel model)
    {
        var command = new FilmUpdateCommand(id, model);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new FilmDeleteCommand(id);
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 5)
    {
        var query = new FilmGetAllQuery(skip, take);
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

    [HttpGet("Years")]
    public async Task<IActionResult> GetYears()
    {
        var query = new FilmYearsGetQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}