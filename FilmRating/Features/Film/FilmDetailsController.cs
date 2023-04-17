using FilmRating.Features.Film.GetDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmRating.Features.Film;


[ApiController]
// TODO Uncomment it when auth feature will be implemented [Authorize]
[Route("api/[controller]")]
public class FilmDetailsController : Controller
{
    private readonly IMediator mediator;

    public FilmDetailsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        Console.WriteLine($"Getting details for id {id}");
        var query = new FilmGetDetailsQuery(id);
        var result = await mediator.Send(query);
        Console.WriteLine($"Returning {result.ToString()}");
        return Ok(result);
    }
}
