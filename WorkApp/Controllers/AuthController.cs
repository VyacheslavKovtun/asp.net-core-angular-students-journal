using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkApp.Business.DTO;
using WorkApp.Business.Services.Users;
using WorkApp.Database.Entities;
using WorkApp.Models.Auth;
using WorkApp.Utils;
using static WorkApp.Database.Entities.User;

namespace WorkApp.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        UsersService usersService;

        public AuthController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel viewModel)
        {
            var identity = GetIdentity(viewModel.Login, viewModel.Password);
            if (identity is null)
                return BadRequest(new { errorText = "Invalid login or password." });

            var role = GetRole(viewModel.Login, viewModel.Password);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromSeconds(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonResult(new
            {
                access_token = encodedJwt,
                username = identity.Name,
                role = role
            });
        }

        private AuthRole GetRole(string login, string password)
        {
            var user = usersService.GetUserByLoginData(login, password).Result;
            return user.Role;
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            var user = usersService.GetUserByLoginData(login, password).Result;
            if (user is null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, Enum.GetName(typeof(User.AuthRole), user.Role))
            };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
