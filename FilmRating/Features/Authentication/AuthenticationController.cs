using Microsoft.AspNetCore.Mvc;

namespace FilmRating.Features.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IIdentityService identityService;

    public AuthenticationController(IIdentityService identityService)
    {
        this.identityService = identityService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var result = await identityService.Register(model);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var result = await identityService.Login(model.Email, model.Password);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}