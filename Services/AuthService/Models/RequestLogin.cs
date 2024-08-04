using System.ComponentModel.DataAnnotations;

namespace BaseProjectDotnet.Services.AuthService.Models;

public class RequestLogin
{
  [Required]
  public string username { get; set; }
  [Required]
  public string password { get; set; }
}
