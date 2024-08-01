namespace BaseProjectDotnet.Helpers.Database.Procedure;

public abstract class StaticSp
{
  // User Authentication & Login
  public const string? StpUserAuth = "[dbo].[SP_UserAuthentication]";
  public const string? StpRolesUpsert = "[dbo].[SP_UserRolesUpsert]";
  public const string? StpRolesDetail = "[dbo].[SP_UserRolesDetail]";
  public const string? StpUserAccessList = "[dbo].[SP_UserAccessList]";

  // Audit Trail
  public const string? StpAuditTrailIndex = "[dbo].[SP_AuditTrailIndex]";
  public const string? StpAuditTrailIndexCount = "[dbo].[SP_AuditTrailIndexCount]";
  public const string? StpAuditTrailCreate = "[dbo].[SP_AuditTrailCreate]";
  public const string? StpAuditTrailDetail = "[dbo].[SP_AuditTrailDetail]";
  public const string? StpAuditTrailArchived = "[dbo].[SP_AuditTrailArchived]";
  public const string? StpAuditTrailRestore = "[dbo].[SP_AuditTrailRestore]";

  // Users
  public const string? StpUserIndex = "[dbo].[SP_UserIndex]";
  public const string? StpUserIndexCount = "[dbo].[SP_UserIndexCount]";
  public const string? StpUserUpsert = "[dbo].[SP_UserUpsert]";
  public const string? StpUserDetail = "[dbo].[SP_UserDetail]";
  public const string? StpUserDelete = "[dbo].[SP_UserDelete]";
  public const string? StpUserRestore = "[dbo].[SP_UserRestore]";

  // Person
  public const string? StpPersonIndex = "[dbo].[SP_PersonIndex]";
  public const string? StpPersonIndexCount = "[dbo].[SP_PersonIndexCount]";
  public const string? StpPersonUpsert = "[dbo].[SP_PersonUpsert]";
  public const string? StpPersonDelete = "[dbo].[SP_PersonDelete]";
  public const string? StpPersonRestore = "[dbo].[SP_PersonRestore]";

  // Select2
  public const string? StpRolesSelect2 = "[dbo].[SP_MasterRoleSelect2]";
  public const string? StpStatusSelect2 = "[dbo].[SP_MasterStatusSelect2]";
}
