using BaseProjectDotnet.Models;
using BaseProjectDotnet.Services.PersonService.Model;

namespace BaseProjectDotnet.Services.PersonService;

public interface IPersonService
{
    DbResponseResult Upsert(List<PersonModel>? model);
}