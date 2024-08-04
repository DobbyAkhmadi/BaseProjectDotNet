using BaseProjectDotnet.Helpers.Enum;
using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.AuditService;
using BaseProjectDotnet.Services.UserService;
using BaseProjectDotnet.Services.UserService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectDotnet.Controllers;

[ApiController]
[Route("/internal/[controller]")]
[Authorize]
public class UserController(IUserService _userService, IAuditTrailService _audit) : Controller
{
  public IActionResult Index()
  {
    return View();
  }

  [HttpGet("get-form")]
  public IActionResult Form(string id, int type)
  {
    ViewBag.FormStatus = type;
    return View("~/Views/User/Modals/ModalDetail.cshtml");
  }

  [HttpPost("index-post")]
  public IActionResult Index([FromBody] DataTableRequestModel args)
  {
    try
    {
      return Ok(_userService.Index(args));
    }
    catch (Exception exception)
    {
      return Json(new DataTableResultModel()
      {
        recordsTotal = 0,
        data = new List<UserDataTableModel>(),
        draw = args.draw,
        error = exception.Message
      });
    }
  }

  [HttpPost]
  [Route("upsert")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult Upsert([FromBody] UserModel model)
  {
    ResponseResultModel res;
    try
    {
      // set required
      var requiredFields = new Dictionary<string, string>
      {
        { nameof(model.id_roles), model.id_roles },
        { nameof(model.full_name), model.full_name },
        { nameof(model.user_name), model.user_name },
        { nameof(model.email), model.email },
        { nameof(model.password), model.password }
      };

      foreach (var field in requiredFields.Where(field => string.IsNullOrEmpty(field.Value)))
      {
        ModelState.AddModelError(field.Key, "This field is required!");
      }

      if (ModelState.IsValid)
      {
        res = _userService.Upsert(model);

        if (res.Success)
        {
          var newModel = _userService.GetById(res.Payload?.ToString());
          //   AuditTrail.SaveAuditTrail(model, null, newModel, "Create Pangkalan", "PANGKALAN.CREATE", UserIdentity.IdUser.ToString(), UserIdentity.RoleName.ToString(), GroupAccessAuditTail.AuditTrailGroupAccessDDMS);
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

  [HttpGet]
  [Route("detail")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult Detail(string id)
  {
    var res = new ResponseResultModel();
    try
    {
      res.Success = true;
      res.Payload = _userService.GetById(id);
    }
    catch (Exception ex)
    {
      res.Success = false;
      res.Message = ex.Message;
    }

    return Ok(res);
  }

  [HttpPost]
  [Route("delete")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult Delete([FromForm] string id)
  {
    ResponseResultModel res = new ResponseResultModel();
    try
    {
      if (ModelState.IsValid)
      {
        //    DbResponseResult oldModel = _userService.GetById(model.id);
        res = _userService.Delete(id);
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
        res = _userService.Restore(id);
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
  [Route("change-password")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult Change([FromForm] string user_id, string new_password)
  {
    ResponseResultModel res;
    try
    {
      if (ModelState.IsValid)
      {
        //    DbResponseResult oldModel = _userService.GetById(model.id);
        res = _userService.ChangePassword(user_id, new_password);
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

  [Route("Roles")]
  public IActionResult Roles()
  {
    return View("Roles/Index");
  }

  [HttpPost("Roles/index-post")]
  public IActionResult RolesIndex([FromBody] DataTableRequestModel args)
  {
    try
    {
      return Ok(_userService.RolesIndex(args));
    }
    catch (Exception exception)
    {
      return Json(new DataTableResultModel()
      {
        recordsTotal = 0,
        data = new List<RoleDataTableModel>(),
        draw = args.draw,
        error = exception.Message
      });
    }
  }

  [HttpPost]
  [Route("Roles/delete")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult RolesDelete([FromForm] string id)
  {
    ResponseResultModel res = new ResponseResultModel();
    try
    {
      if (ModelState.IsValid)
      {
        //    DbResponseResult oldModel = _userService.GetById(model.id);
        res = _userService.RolesDelete(id);
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
  [Route("Roles/restore")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult RolesRestore([FromForm] string id)
  {
    ResponseResultModel res = new ResponseResultModel();
    try
    {
      if (ModelState.IsValid)
      {
        //    DbResponseResult oldModel = _userService.GetById(model.id);
        res = _userService.RolesRestore(id);
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

  [Route("Permission")]
  public IActionResult Permission()
  {
    return View("Permission/Index");
  }

  [HttpPost("Permission/index-post")]
  public IActionResult PermissionIndex([FromBody] DataTableRequestModel args)
  {
    try
    {
      return Ok(_userService.PermissionIndex(args));
    }
    catch (Exception exception)
    {
      return Json(new DataTableResultModel()
      {
        recordsTotal = 0,
        data = new List<PermissionDataTableModel>(),
        draw = args.draw,
        error = exception.Message
      });
    }
  }

  [HttpPost]
  [Route("Permission/delete")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult PermissionDelete([FromForm] string id)
  {
    ResponseResultModel res = new ResponseResultModel();
    try
    {
      if (ModelState.IsValid)
      {
        //    DbResponseResult oldModel = _userService.GetById(model.id);
        res = _userService.PermissionDelete(id);
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
  [Route("Permission/restore")]
  [ProducesResponseType(typeof(ResponseResultModel), 200)]
  [ProducesResponseType(500)]
  public IActionResult PermissionRestore([FromForm] string id)
  {
    ResponseResultModel res = new ResponseResultModel();
    try
    {
      if (ModelState.IsValid)
      {
        //    DbResponseResult oldModel = _userService.GetById(model.id);
        res = _userService.PermissionRestore(id);
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
