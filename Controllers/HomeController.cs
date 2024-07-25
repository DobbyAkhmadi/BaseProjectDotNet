using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
