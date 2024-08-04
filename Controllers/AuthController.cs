using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BaseProjectDotnet.Services.AuthService;
using BaseProjectDotnet.Services.AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BaseProjectDotnet.Controllers;

public class AuthController(IConfiguration _config, IAuthService authService,HttpClient _httpClient) : Controller
{
  public IActionResult Index()
  {
    return View();
  }

  [HttpPost("login-post")]
  public IActionResult AuthLogin([FromForm] RequestLogin request)
  {
    try
    {
      if (ModelState.IsValid)
      {
        var res = authService.ValidateCredentials(request);

        if (res.Payload == "")
        {
          res.Success = false;

          return Ok(res);
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var securityToken = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          claims: null,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        res.Payload = token;
        res.Success = true;

        // Create cookie options
        var cookieOptions = new CookieOptions
        {
          HttpOnly = true,
         // Secure = true, // Set to true if using HTTPS
          SameSite = SameSiteMode.Strict,
          Expires = DateTimeOffset.Now.AddMinutes(120)
        };

        // Append the cookie
        Response.Cookies.Append("JwtToken", token, cookieOptions);

        return Ok(res);

      }
    }
    catch (Exception e)
    {
      return StatusCode(500, e.Message);
    }

    return BadRequest(ModelState);
  }

  [HttpPost("logout-post")]
  public IActionResult AuthLogout()
  {
    // Clear the specific cookie
    Response.Cookies.Delete("JwtToken");

    // Redirect or return a response
    return RedirectToAction("Index", "Auth");
  }

}
