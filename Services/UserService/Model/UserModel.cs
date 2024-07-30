namespace BaseProjectDotnet.Services.UserService.Model;

public class UserDataTableModel
{
  public string? id { get; set; }
  public string? role_name { get; set; }
  public string? full_name { get; set; }
  public string? user_name { get; set; }
  public string? email { get; set; }
  public string? type_active { get; set; }
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

public class Roles
{
  public string id;
  public string name;
  public string description;
  public List<PermissionAccess>? PermissionAccess = [];
  public int type_active;
}

public class PermissionAccess
{
  public string permission_id;
  public string name;
  public string module_name;
  public string description;
  public int type_active;
}
