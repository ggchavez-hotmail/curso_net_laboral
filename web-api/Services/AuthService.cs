using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace web_api.Services
{
    public interface IAuthService
    {
        Task<string> SignIn(LoginParameters param);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> SignIn(LoginParameters param)
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
                return await GenerarTokenAsync(user);
            }
            else
            {
                throw new Exception("Usuario o contraseña invalida");
            }
        }
        public async Task<string> GenerarTokenAsync(IdentityUser user)
        {
            var cl = await _userManager.GetClaimsAsync(user);
            cl.Add(new Claim(ClaimTypes.Name, user.UserName));

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                cl.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityTokenHandler hander = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:key"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(cl),
                NotBefore = DateTime.Now.Subtract(TimeSpan.FromMinutes(20)),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var token = hander.CreateToken(tokenDescriptor);
            return hander.WriteToken(token);

        }

    }
    public class LoginParameters
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
