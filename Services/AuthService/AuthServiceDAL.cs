using System.Data.SqlClient;
using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Helpers.Database.Procedure;
using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.AuthService.Models;
using BaseProjectDotnet.Services.UserService.Model;

namespace BaseProjectDotnet.Services.AuthService;

public class AuthServiceDAL (DatabaseContext _context): IAuthService
{
  public ResponseResultModel ValidateCredentials(RequestLogin request)
  {
    var result = new ResponseResultModel();

    using var sqlConnection = _context.DConnection1();
    using var sqlCommand = new SqlCommand(StaticSp.StpUserAuth, sqlConnection);
    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

    sqlCommand.Parameters.AddWithValue("@Username", request.username);
    sqlCommand.Parameters.AddWithValue("@Password", request.password);

    sqlConnection.Open();

    using (var dataReader = sqlCommand.ExecuteReader())
    {
      while (dataReader.Read())
      {
        result.Success = Convert.ToBoolean(dataReader["Code"]);
        result.Message = dataReader["Message"].ToString();
        result.Payload = dataReader["Payload"].ToString();
      }

      dataReader.Close();
    }

    sqlConnection.Close();

    return result;
  }

  public UserModel GetUserAccess(string user_id)
  {
    throw new NotImplementedException();
  }

  public Roles GetRolesById(string role_id)
  {
    throw new NotImplementedException();
  }

  public List<PermissionAccess> GetAccessList(string role_id, string user_id)
  {
    throw new NotImplementedException();
  }
}
