using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

public class AuditController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}
