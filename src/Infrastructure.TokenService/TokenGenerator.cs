using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Abstractions.Services;

namespace TokenService;


/// <summary>
/// Service for generating tokens
/// </summary>
public class TokenGenerator : ITokenGenerator
{
    /// <summary>
    /// Key for encryption
    /// </summary>
    private readonly SymmetricSecurityKey _securityKey;

    /// <summary>
    /// Initialize new instance of <see cref="TokenGenerator"/>
    /// </summary>
    /// <param name="key"> Key for encryption </param>
    public TokenGenerator(string key)
    {
        _securityKey = new(Encoding.UTF8.GetBytes(key));
    }

    /// <summary>
    /// Generate token
    /// </summary>
    /// <param name="email">Email of <see cref="User"/> credentials</param>
    /// <returns>New token</returns>
    public string Generate(string email)
    {
        var claims = new[] { new Claim(ClaimTypes.Name, email) };
        var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken("Server", "Client", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
