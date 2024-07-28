namespace BaseProjectDotnet.Helpers.Global;

public static class GlobalVariable
{
  public const string App_Title = "Internal Systems Apps";
  private const string App_Internal_Slugs = "internal";

  // MENU
  public const string UrlInternalHome = "/"+App_Internal_Slugs+"/home";
  public const string UrlInternalAudit = "/"+App_Internal_Slugs+"/Audit";

  // USERS
  public const string UrlInternalUsers = "/"+App_Internal_Slugs+"/User";
  public const string UrlInternalUserRoles = "/"+App_Internal_Slugs+"/User/Roles";
  public const string UrlInternalUserPermission = "/"+App_Internal_Slugs+"/User/Permission";

}
