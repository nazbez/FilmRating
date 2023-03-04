using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmRating.Features.Films;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FilmController : Controller
{
    [HttpGet("Test")]
    public IActionResult Test() => Ok("Test");
}