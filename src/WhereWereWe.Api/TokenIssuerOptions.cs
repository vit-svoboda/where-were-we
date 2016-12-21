using Microsoft.IdentityModel.Tokens;
using System;

namespace WhereWereWe.Api
{
    public class TokenIssuerOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan ValidFor { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }
}
