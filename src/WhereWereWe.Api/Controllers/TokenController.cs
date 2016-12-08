using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using WhereWereWe.Api.Models;
using System.Threading.Tasks;
using System.Security.Principal;

namespace WhereWereWe.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly TokenIssuerOptions options;

        public TokenController(IOptions<TokenIssuerOptions> options)
        {
            this.options = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ApplicationUser user)
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

        private Task<ClaimsIdentity> GetIdentity(ApplicationUser user)
        {
            if (user.UserName == "test")
            {
                return Task.FromResult(new ClaimsIdentity(
                    new GenericIdentity(user.UserName, "Token"),
                    new Claim[] { }));
            }

            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
