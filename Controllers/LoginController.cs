using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

public class LoginController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}
