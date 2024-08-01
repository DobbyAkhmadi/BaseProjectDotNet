using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BaseProjectDotnet.Services.LoginService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BaseProjectDotnet.Controllers;
[Route("/internal/[controller]")]
[ApiController]
public class LoginController(IConfiguration _config) : Controller
{
  public IActionResult Index()
  {
    return View();
  }

  [HttpPost("login")]
  public IActionResult AuthLogin([FromBody] LoginRequest loginRequest)
  {
    //your logic for login process
    //If login usrename and password are correct then proceed to generate token

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var securityToken = new JwtSecurityToken(_config["Jwt:Issuer"],
      _config["Jwt:Issuer"],
      null,
      expires: DateTime.Now.AddMinutes(120),
      signingCredentials: credentials);

    var token =  new JwtSecurityTokenHandler().WriteToken(securityToken);

    return Ok(token);
  }
}
