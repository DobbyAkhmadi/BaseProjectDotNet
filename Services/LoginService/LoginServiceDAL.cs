using System.Data.SqlClient;
using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Helpers.Database.Procedure;
using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.LoginService.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace BaseProjectDotnet.Services.LoginService;

public class LoginServiceDAL (DatabaseContext _context): ILoginService
{
  public bool ValidateCredentials(RequestLogin requestLogin)
  {
    try
    {
      DbResponseResult Result = new();

      using var sqlConnection = _context.DConnection1();
      using var sqlCommand = new SqlCommand(StaticSp.StpAuditTrailArchived, sqlConnection);
      sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

      sqlCommand.Parameters.AddWithValue("@Username", requestLogin.username);
      sqlCommand.Parameters.AddWithValue("@Password", requestLogin.password);

      sqlConnection.Open();

      using (var dataReader = sqlCommand.ExecuteReader())
      {
        while (dataReader.Read())
        {
          Result.Success = Convert.ToBoolean(dataReader["Code"]);
          Result.Message = dataReader["Message"].ToString();
          Result.Payload = dataReader["Payload"].ToString();
        }

        dataReader.Close();
      }

      sqlConnection.Close();

      return Result;
    }
    catch (Exception)
    {
      throw;
    }
  }
}
