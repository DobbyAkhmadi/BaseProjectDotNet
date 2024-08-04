using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.AuditService;
using BaseProjectDotnet.Services.AuditService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

[ApiController]
[Route("/internal/[controller]")]
[Authorize]
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

  [HttpGet]
  [Route("detail")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult Detail( string id)
  {
    var res = new ResponseResultModel();
    try
    {
      res.Success = true;
      res.Payload = _auditTrailService.GetById(id);
    }
    catch (Exception ex)
    {
      res.Success = false;
      res.Message = ex.Message;
    }

    return Ok(res);
  }
  [HttpPost]
  [Route("archive")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult Archive([FromForm] string id)
  {
    ResponseResultModel res;
    try
    {
      if (ModelState.IsValid)
      {
        //    DbResponseResult oldModel = _userService.GetById(model.id);
        res = _auditTrailService.Archived(id);
        if (res.Success)
        {
          //  AuditTrail.SaveAuditTrail(model, oldModel, null, "Delete Pangkalan", "PANGKALAN.DELETE", UserIdentity.IdUser.ToString(), UserIdentity.RoleName.ToString(), GroupAccessAuditTail.AuditTrailGroupAccessDDMS);
        }
      }
      else
      {
        return BadRequest(ModelState);
      }
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }

    return Ok(res);
  }

  [HttpPost]
  [Route("restore")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult Restore([FromForm] string id)
  {
    ResponseResultModel res;
    try
    {
      if (ModelState.IsValid)
      {
        //    DbResponseResult oldModel = _userService.GetById(model.id);
        res = _auditTrailService.Restore(id);
        if (res.Success)
        {
          //  AuditTrail.SaveAuditTrail(model, oldModel, null, "Delete Pangkalan", "PANGKALAN.DELETE", UserIdentity.IdUser.ToString(), UserIdentity.RoleName.ToString(), GroupAccessAuditTail.AuditTrailGroupAccessDDMS);
        }
      }
      else
      {
        return BadRequest(ModelState);
      }
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }

    return Ok(res);
  }
}
