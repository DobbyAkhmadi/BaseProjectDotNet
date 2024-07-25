namespace BaseProjectDotnet.Services.UserService.Model;

public class Users
{
  public string user_id;
  public string user_name;
  public string password;
  public string type_active;
  public List<Roles> RolesList = new();
}

public abstract class Roles
{
  public int role_id;
  public string role_name;
  public List<PermissionAccess> PermissionAccess = new();
  public string type_active;
}

public abstract class PermissionAccess
{
  public int permission_id;
  public string module_name;
  public string type_active;
}
