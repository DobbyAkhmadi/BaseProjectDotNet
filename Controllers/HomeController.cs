using System.Diagnostics;
using BaseProjectDotnet.Services.PersonService;
using BaseProjectDotnet.Services.PersonService.Model;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

public class HomeController(ILogger<HomeController> logger, IPersonService personService) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("save")]
    public IActionResult Save(List<PersonModel>? persons)
    {
        Debug.Assert(persons != null, nameof(persons) + " != null");
        if (persons.Count == 0)
        {
            return BadRequest("Persons is empty");
        }

        return Json(personService.Upsert(persons));
    }
}