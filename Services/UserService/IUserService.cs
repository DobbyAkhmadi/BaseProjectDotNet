using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.UserService.Model;

namespace BaseProjectDotnet.Services.UserService;

public interface IUserService
{
  DataTableResultModel Index(DataTableRequestModel args);
  DataTableResultModel RolesIndex(DataTableRequestModel args);
  DataTableResultModel PermissionIndex(DataTableRequestModel args);
  ResponseResultModel Upsert(UserModel userModel);
  UserModel GetById(string? id);
  ResponseResultModel Delete(string id);
  ResponseResultModel Restore(string id);
  ResponseResultModel ChangePassword(string user_id, string new_password);
}
