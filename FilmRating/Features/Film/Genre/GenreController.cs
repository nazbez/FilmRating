using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmRating.Features.Film.Genre;

[ApiController]
[Route("api/[controller]")]
public class GenreController : Controller
{
    private readonly IMediator mediator;

    public GenreController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GenreGetAllQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}