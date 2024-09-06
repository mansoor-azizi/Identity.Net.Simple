using Identity.Net.Simple.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Identity.Net.Simple.Controllers;


// Use Authorize Attribute to Verify Identity User Logged In 
// and wherever you need to authenticate
[Authorize]

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetOnlineUserInfo")]
    public async Task<IActionResult> GetOnlineUserInfo()
    {
        var userIdentity = (User.Identity as ClaimsIdentity);

        return Ok(new { 
            IsAutenticated= userIdentity.IsAuthenticated,
            UserName = userIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            UserId= userIdentity.FindFirst("UserId")?.Value,
            FirsName = userIdentity.FindFirst("FirsName")?.Value,
            LastName = userIdentity.FindFirst("LastName")?.Value,
        });
    }
}

