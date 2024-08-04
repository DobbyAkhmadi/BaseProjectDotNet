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

public class RoleDataTableModel
{
  public string? id { get; set; }
  public string? name { get; set; }
  public string? description { get; set; }
  public string? type_active { get; set; }
}

public class PermissionDataTableModel
{
  public string? id { get; set; }
  public string? name { get; set; }
  public string? router { get; set; }
  public string? description { get; set; }
  public string? type_active { get; set; }
}

public class UserModel
{
  public string? id { get; set; }
  public string? id_roles { get; set; }
  public string? role_name { get; set; }
  public string? full_name { get; set; }
  public string? user_name { get; set; }
  public string password { get; set; }
  public string? email { get; set; }
  public Roles? Roles { get; set; }
  public string? type_active { get; set; }
}

public class Roles
{
  public string? id { get; set; }
  public string? name { get; set; }
  public string? description { get; set; }
  public List<PermissionAccess>? PermissionAccess  { get; set; }
  public string? type_active;
}

public class PermissionAccess
{
  public string? id { get; set; }
  public string? name { get; set; }
  public string? module_name { get; set; }
  public string? description { get; set; }
  public string? type_active { get; set; }
}
