using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.MasterService;
using BaseProjectDotnet.Services.MasterService.Modals;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

[ApiController]
[Route("/internal/[controller]")]
public class MasterController(IMasterService _masterService) : Controller
{
  [HttpGet("roles-select2")]
  public IActionResult? RolesSelect2()
  {
    try
    {
      var term = Request.Query["q[term]"].ToString();
      var page = Request.Query["q[page]"].ToString();

      return Json(_masterService.RolesSelect2(term,page));
    }
    catch (Exception)
    {
      return Json(new DbResponseResult()
      {
        Success = true,
        Message = "Roles Select2",
        Payload = new List<Select2ResultModel>(),
      });
    }
  }

  [HttpGet("status-select2")]
  public IActionResult? StatusSelect2()
  {
    try
    {
      var term = Request.Query["q[term]"].ToString();
      var page = Request.Query["q[page]"].ToString();

      return Json(_masterService.StatusSelect2(term,page));
    }
    catch (Exception)
    {
      return Json(new DbResponseResult()
      {
        Success = true,
        Message = "Status Select2",
        Payload = new List<Select2ResultModel>(),
      });
    }
  }
}
