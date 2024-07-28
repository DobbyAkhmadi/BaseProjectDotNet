using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

[ApiController]
[Route("/internal/[controller]")]
public class HomeController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}
