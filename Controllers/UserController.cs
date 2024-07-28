using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Services.AuditService.Model;
using BaseProjectDotnet.Services.UserService;
using BaseProjectDotnet.Services.UserService.Model;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

[ApiController]
[Route("/internal/[controller]")]
public class UserController(IUserService _userService) : Controller
{
  public IActionResult Index()
  {
    return View();
  }

  [HttpPost("index-post")]
  public IActionResult Index([FromBody] DataTableRequestModel args)
  {
  //  try
    //{
    var response = _userService.Index(args);
      return Ok(response);
    //}
    // catch (Exception exception)
    // {
    //   return Json(new DataTableResultModel()
    //   {
    //     recordsTotal = 0,
    //     data = new List<UserDataTableModel>(),
    //     draw = args.draw,
    //     error = exception.Message
    //   });
    // }
  }

  [Route("Roles")]
  public IActionResult Roles()
  {
    return View("Roles/Index");
  }

  [Route("Permission")]
  public IActionResult Permission()
  {
    return View("Permission/Index");
  }
}
