using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers.Sistema
{
    [Route("api/sistema/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UsuarioController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> QuienSoyAsync()
        {
            var ident = User.Identity as ClaimsIdentity;

            var user = await _userManager.FindByNameAsync(ident.Name);
            return Ok(user);
        }
    }
}
