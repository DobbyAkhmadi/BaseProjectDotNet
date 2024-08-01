using BaseProjectDotnet.Services.LoginService.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace BaseProjectDotnet.Services.LoginService;

public interface ILoginService
{
  public bool ValidateCredentials(RequestLogin requestLogin);

}
