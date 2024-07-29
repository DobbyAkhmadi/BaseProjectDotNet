using System.Data.SqlClient;
using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Helpers.Database.Procedure;
using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.MasterService.Modals;
using static System.String;

namespace BaseProjectDotnet.Services.MasterService;

public class MasterServiceDAL(DatabaseContext _context) : IMasterService
{
  public DbResponseResult RolesSelect2(string term, string page)
  {
    var result = new DbResponseResult();

    var select2 = new List<Select2ResultModel>();

    var sqlConnection = _context.DConnection1();
    using (var sqlCommand = new SqlCommand(StaticSp.StpRolesSelect2, sqlConnection))
    {
      sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
      sqlCommand.Parameters.AddWithValue("@Keywords", (term ?? "").Trim());
      sqlCommand.Parameters.AddWithValue("@Page", IsNullOrEmpty(page) ? 1 : page.Trim());
      sqlConnection.Open();

      using (var dataReader = sqlCommand.ExecuteReader())
      {
        while (dataReader.Read())
        {
          select2.Add(new Select2ResultModel()
          {
            id = dataReader["ID"].ToString()?.Trim(),
            text = dataReader["Name"].ToString()?.Trim(),
          });
        }
      }

      sqlConnection.Close();
    }

    result.Payload = select2;

    return result;
  }

  public DbResponseResult StatusSelect2(string term, string page)
  {
    var result = new DbResponseResult();

    var select2 = new List<Select2ResultModel>();

    var sqlConnection = _context.DConnection1();
    using (var sqlCommand = new SqlCommand(StaticSp.StpStatusSelect2, sqlConnection))
    {
      sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
      sqlCommand.Parameters.AddWithValue("@Keywords", (term ?? "").Trim());
      sqlCommand.Parameters.AddWithValue("@Page", IsNullOrEmpty(page) ? 1 : page.Trim());
      sqlConnection.Open();

      using (var dataReader = sqlCommand.ExecuteReader())
      {
        while (dataReader.Read())
        {
          select2.Add(new Select2ResultModel()
          {
            id = dataReader["ID"].ToString()?.Trim(),
            text = dataReader["Name"].ToString()?.Trim(),
          });
        }
      }

      sqlConnection.Close();
    }

    result.Payload = select2;

    return result;
  }
}
