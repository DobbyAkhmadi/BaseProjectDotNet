using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.AuditService.Model;
using BaseProjectDotnet.Services.UserService.Model;

namespace BaseProjectDotnet.Services.UserService;

public interface IUserService
{
  DataTableResultModel Index(DataTableRequestModel args);
  DbResponseResult Upsert(UserModel userModel);
  AuditTrailModel GetById(string id);
  DbResponseResult Delete(string id);
  DbResponseResult Restore(string id);
}
