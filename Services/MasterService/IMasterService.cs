using BaseProjectDotnet.Helpers.Global.Models;

namespace BaseProjectDotnet.Services.MasterService;

public interface IMasterService
{
  ResponseResultModel RolesSelect2(string term, string page);

  ResponseResultModel StatusSelect2(string term, string page);
}
