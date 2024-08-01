using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BaseProjectDotnet.Helpers.Middleware;
public class JwtMiddleware(IConfiguration config, RequestDelegate next)
{
  public async Task InvokeAsync(HttpContext context)
  {
    if (context.Request.Cookies.TryGetValue("JwtToken", out var token))
    {
      var principal = ValidateToken(token);
      context.User = principal;
    }

    await next(context);
  }

  private ClaimsPrincipal ValidateToken(string token)
  {
    var key = Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? string.Empty);
    var issuer = config["Jwt:Issuer"];
    var tokenHandler = new JwtSecurityTokenHandler();
    var validationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = issuer,
      ValidAudience = issuer,
      IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    try
    {
      return tokenHandler.ValidateToken(token, validationParameters, out _);
    }
    catch
    {
      return null;
    }
  }
}
