using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api.Services;

namespace web_api.Controllers.Sistema
{
    [Route("api/sistema/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginParameters param)
        {
            bool result = await _authService.SignIn(param);
            return Ok(result);
        }
    }
}
