using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.AuditService.Model;

namespace BaseProjectDotnet.Services.AuditService;

public interface IAuditTrailService
{
  DataTableResultModel Index(DataTableRequestModel args);
  ResponseResultModel SaveAudit(AuditTrailModel auditTrailModel);
  AuditTrailModel GetById(string id);
  ResponseResultModel Archived(string id);
  ResponseResultModel Restore(string id);
}
