using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace web_api.Services
{
    public interface IAuthService
    {
        Task<bool> SignIn(LoginParameters param);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> SignIn(LoginParameters param)
        {
            if (param.UserName == null)
            {
                string message = $"No se ingreso un usuario";
                throw new ArgumentNullException(nameof(param.UserName), message);
            }
            if (param.Password == null)
            {
                string message = $"No se ingreso una contraseña";
                throw new ArgumentNullException(nameof(param.Password), message);
            }

            IdentityUser user = await _userManager.FindByNameAsync(param.UserName);
            if (user == null)
            {
                throw new Exception("Usuario o contraseña invalida");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, param.Password, true);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new Exception("Usuario o contraseña invalida");
            }
        }
    }
    public class LoginParameters
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
