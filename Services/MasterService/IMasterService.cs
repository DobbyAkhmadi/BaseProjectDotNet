using BaseProjectDotnet.Helpers.Global.Models;

namespace BaseProjectDotnet.Services.MasterService;

public interface IMasterService
{
  DbResponseResult RolesSelect2(string term, string page);

  DbResponseResult StatusSelect2(string term, string page);
}
