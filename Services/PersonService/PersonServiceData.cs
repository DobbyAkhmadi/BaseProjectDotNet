using System.Data;
using System.Data.SqlClient;
using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Helpers.Database.Procedure;
using BaseProjectDotnet.Helpers.Database.UDT;
using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Models;
using BaseProjectDotnet.Services.PersonService.Model;

namespace BaseProjectDotnet.Services.PersonService;

internal class PersonServiceData(DatabaseContext context) : IPersonService
{
  public DbResponseResult Upsert(PersonModel? model)
  {
    var connection = context.DConnection1();
    var result = new DbResponseResult();

    using var command = new SqlCommand(StaticSp.StpPersonUpsert, connection);
    command.CommandType = CommandType.StoredProcedure;

    if (model != null)
    {
      command.Parameters.Add(UpsertUdt(model));
    }

    try
    {
      connection.Open();

      using var reader = command.ExecuteReader();
      while (reader.Read())
      {
        result.Success = Convert.ToBoolean(reader["Code"]);
        result.Message = reader["Message"].ToString();
        result.Payload = reader["Payload"].ToString();
      }

      reader.Close();
    }
    catch (Exception e)
    {
      result.StatusCode = 500;
      result.Message = $"An error occurred: {e.Message}";
    }
    finally
    {
      connection.Close();
    }

    return result;
  }

  private static SqlParameter UpsertUdt(PersonModel model)
  {
    if (model.PersonList.Count < 0)
    {
      return new SqlParameter("@personList", null);
    }

    var table = new DataTable();
    table.Columns.Add(new DataColumn("ID", typeof(int)));
    table.Columns.Add(new DataColumn("Name", typeof(string)));

    foreach (var person in model.PersonList)
    {
      table.Rows.Add(person.IdDetail, person.Name);
    }

    SqlParameter sqlParameter;

    try
    {
      sqlParameter = new SqlParameter("@personList", SqlDbType.Structured)
      {
        TypeName = TypeUdt.UdtPerson,
        Value = table
      };
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }

    if (sqlParameter == null)
    {
      throw new ArgumentNullException(nameof(sqlParameter));
    }

    return sqlParameter;
  }
}
