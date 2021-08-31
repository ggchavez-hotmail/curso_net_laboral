using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_api.Services;

namespace web_api.Controllers.Sistema
{
    [Route("api/sistema/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("SignUpAdministrador")]
        public async Task<IActionResult> SignUpAdministrador([FromBody] SignUpParameters param)
        {
            IEnumerable<IdentityError> errors = await _accountService.CrearAdminAsync(param);

            if (errors == null)
            {
                return Ok();
            }
            else
            {
                return BadRequest(errors);
            }
        }

        [HttpPost("SignUpVisita")]
        public async Task<IActionResult> SignUpVisita([FromBody] SignUpParameters param)
        {
            IEnumerable<IdentityError> errors = await _accountService.CrearVisitAsync(param);

            if (errors == null)
            {
                return Ok();
            }
            else
            {
                return BadRequest(errors);
            }
        }
    }
}
