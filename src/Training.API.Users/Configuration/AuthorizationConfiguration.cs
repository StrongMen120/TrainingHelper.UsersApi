using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Training.API.Users;
public class AuthorizationConfiguration
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
    public TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = this.Issuer,
            ValidAudience = this.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Secret))
        };
    }
}