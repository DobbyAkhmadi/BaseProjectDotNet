using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.PersonService.Model;

namespace BaseProjectDotnet.Services.PersonService;

public interface IPersonService
{
    DbResponseResult Upsert(PersonModel? model);
}
