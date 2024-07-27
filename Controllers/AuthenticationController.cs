using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

public class AuthenticationController :Controller
{
  public IActionResult Index()
  {
    return View();
  }
}
