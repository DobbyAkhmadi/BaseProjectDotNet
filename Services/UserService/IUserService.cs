using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.AuditService.Model;
using BaseProjectDotnet.Services.UserService.Model;

namespace BaseProjectDotnet.Services.UserService;

public interface IUserService
{
  DataTableResultModel Index(DataTableRequestModel args);
  ResponseResultModel Upsert(UserModel userModel);
  UserModel GetById(string? id);
  ResponseResultModel Delete(string id);
  ResponseResultModel Restore(string id);
}
