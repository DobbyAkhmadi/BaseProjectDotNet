namespace BaseProjectDotnet.Services.UserService.Model;

public class UserDataTableModel
{
  public string? id;
  public string? role_name;
  public string? full_name;
  public string? user_name;
  public string? email;
  public string? type_active;
}

public class UserModel
{
  public string? id;
  public string? id_roles;
  public string? role_name;
  public string? full_name;
  public string? user_name;
  public string password;
  public string? email;
  public Roles? Roles;
  public int type_active;
}

public abstract class Roles
{
  public string id;
  public string name;
  public string description;
  public List<PermissionAccess>? PermissionAccess = new();
  public int type_active;
}

public abstract class PermissionAccess
{
  public string permission_id;
  public string name;
  public string module_name;
  public string description;
  public int type_active;
}
