using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Security.Principal;
using WhereWereWe.Api.Models;
using WhereWereWe.Domain.Interfaces;

namespace WhereWereWe.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly TokenIssuerOptions options;
        private readonly IUserRepository userRepository;

        public TokenController(IOptions<TokenIssuerOptions> options, IUserRepository userRepository)
        {
            this.options = options.Value;
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginViewModel user)
        {
            var identity = await GetIdentity(user);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            var timestamp = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, timestamp.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: timestamp,
                expires: timestamp.Add(options.ValidFor),
                signingCredentials: options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = options.ValidFor.TotalSeconds
            };

            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(LoginViewModel user)
        {
            var existingUser = await userRepository.ValidateLogin(user.UserName, user.Password);
            if (existingUser != null)
            {
                return new ClaimsIdentity(
                    new GenericIdentity(existingUser.Name, "Token"),
                    new Claim[] { });
            }

            return null;
        }
    }
}
