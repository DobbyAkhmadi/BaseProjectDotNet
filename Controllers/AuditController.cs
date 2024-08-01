using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.AuditService;
using BaseProjectDotnet.Services.AuditService.Model;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

[ApiController]
[Route("/internal/[controller]")]
public class AuditController(IAuditTrailService _auditTrailService) : Controller
{
  public IActionResult Index()
  {
    return View();
  }

  [HttpPost("index-post")]
  public IActionResult Index([FromBody] DataTableRequestModel args)
  {
    try
    {
      return Ok(_auditTrailService.Index(args));
    }
    catch (Exception exception)
    {
      return Json(new DataTableResultModel()
      {
        recordsTotal = 0,
        data = new List<AuditTrailModel>(),
        draw = args.draw,
        error = exception.Message
      });
    }
  }
}
