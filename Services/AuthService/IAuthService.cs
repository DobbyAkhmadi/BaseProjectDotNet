using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.LoginService.Models;
using BaseProjectDotnet.Services.UserService.Model;

namespace BaseProjectDotnet.Services.AuthService;

public interface IAuthService
{
  public ResponseResultModel ValidateCredentials(RequestLogin requestLogin);
  public UserModel GetUserAccess(string user_id);
  public Roles GetRolesById(string role_id);
  public List<PermissionAccess> GetAccessList(string role_id,string user_id);

}
