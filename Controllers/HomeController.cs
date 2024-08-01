using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

[ApiController]
[Route("/internal/[controller]")]

public class HomeController : Controller
{
  [Authorize]
  public IActionResult Index()
  {
    return View();
  }

  [Route("Error")]
  public IActionResult ErrorMsg( int statusCode)
  {
    return statusCode switch
    {
      401 => View("Error/401") // not authorized login
      ,
      403 => View("Error/403") // forbidden access
      ,
      _ => View("Error/Maintenance")
    };
  }
}
