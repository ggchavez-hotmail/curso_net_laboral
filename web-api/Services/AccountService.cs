using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace web_api.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<IdentityError>> CrearAdminAsync(SignUpParameters param);

        Task<IEnumerable<IdentityError>> CrearVisitAsync(SignUpParameters param);

    }

    public class AccountService : IAccountService
    {
        private const string RoleNameAdmin = "Administrador";
        private const string RoleNameVisita = "Visitante";
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<IdentityError>> CrearAdminAsync(SignUpParameters param)
        {
            return await CrearUsuarioAsync(param, RoleNameAdmin);
        }

        public async Task<IEnumerable<IdentityError>> CrearVisitAsync(SignUpParameters param)
        {
            return await CrearUsuarioAsync(param, RoleNameVisita);
        }
        public async Task<IEnumerable<IdentityError>> CrearUsuarioAsync(SignUpParameters param, string roleName)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = param.UserName,
                Email = param.Email,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, param.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    IdentityRole rol = new IdentityRole(roleName);
                    await _roleManager.CreateAsync(rol);
                }
                await _userManager.AddToRoleAsync(user, roleName);

                return null;
            }
            else
            {
                return result.Errors;
            }

        }
    }

    public class SignUpParameters
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
