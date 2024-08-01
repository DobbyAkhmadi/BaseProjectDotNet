using System.ComponentModel.DataAnnotations;

namespace BaseProjectDotnet.Services.LoginService.Models;

public class RequestLogin
{
  [Required]
  public string username { get; set; }
  [Required]
  public string password { get; set; }
}
