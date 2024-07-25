namespace BaseProjectDotnet.Helpers.Database.Procedure;

public abstract class StaticSp
{
  // Audit Trail
  public const string? StpAuditTrailIndex = "[dbo].[SP_AuditTrailIndex]";
  public const string? StpAuditTrailIndexCount = "[dbo].[SP_AuditTrailIndexCount]";
  public const string? StpAuditTrailUpsert = "[dbo].[SP_AuditTrailUpsert]";
  public const string? StpAuditTrailDelete = "[dbo].[SP_AuditTrailDelete]";
  public const string? StpAuditTrailRestore = "[dbo].[SP_AuditTrailRestore]";

  // Person
  public const string? StpPersonIndex = "[dbo].[SP_PersonIndex]";
  public const string? StpPersonIndexCount = "[dbo].[SP_PersonIndexCount]";
  public const string? StpPersonUpsert = "[dbo].[SP_PersonUpsert]";
  public const string? StpPersonDelete = "[dbo].[SP_PersonDelete]";
  public const string? StpPersonRestore = "[dbo].[SP_PersonRestore]";
}
