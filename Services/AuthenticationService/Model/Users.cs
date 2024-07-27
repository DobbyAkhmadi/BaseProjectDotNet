namespace BaseProjectDotnet.Services.AuthenticationService.Model;

public class Users
{
  public int id;
  public int role_id;
  public string full_name;
  public string user_name;
  public string password;
  public string email;
  public Roles? Roles;
  public int type_active;
}

public abstract class Roles
{
  public int id;
  public string name;
  public string description;
  public List<PermissionAccess>? PermissionAccess = new();
  public int type_active;
}

public abstract class PermissionAccess
{
  public int permission_id;
  public string name;
  public string module_name;
  public string description;
  public int type_active;
}
