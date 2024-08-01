using System.Data.SqlClient;
using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Helpers.Database.Procedure;
using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.AuditService.Model;
using BaseProjectDotnet.Services.UserService.Model;

namespace BaseProjectDotnet.Services.UserService;

public class UserServiceData(DatabaseContext _context) : IUserService
{
  public DataTableResultModel Index(DataTableRequestModel args)
  {
    List<UserDataTableModel> Result = [];
    var recordsTotal = UserCount(args);
    try
    {
      using var sqlConnection = _context.DConnection1();
      using var sqlCommand = new SqlCommand(StaticSp.StpUserIndex, sqlConnection);
      sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
      sqlCommand.Parameters.AddWithValue("@Keywords", (args.search.value ?? "").Trim());
      sqlCommand.Parameters.AddWithValue("@FromStart", args.start);
      sqlCommand.Parameters.AddWithValue("@FromEnd", args.length);
      sqlCommand.Parameters.AddWithValue("@TypeActive", args.TypeActive);

      if (args.order is { Count: > 0 })
      {
        sqlCommand.Parameters.AddWithValue("@Sort", args.order[0]?.dir);
        sqlCommand.Parameters.AddWithValue("@FieldIndex", args.order[0].column);
      }

      if (args.advSearch.Count > 0)
      {
        for (var index = 0; index < args.advSearch.Count; index++)
        {
          var item = args.advSearch.ElementAt(index);
          sqlCommand.Parameters.AddWithValue("@" + item.Key, item.Value);
        }
      }

      sqlConnection.Open();

      using (var dataReader = sqlCommand.ExecuteReader())
      {
        while (dataReader.Read())
        {
          var model = new UserDataTableModel
          {
            id = dataReader["id"] != DBNull.Value ? dataReader["id"].ToString() : string.Empty,
            full_name = dataReader["full_name"] != DBNull.Value ? dataReader["full_name"].ToString() : string.Empty,
            user_name = dataReader["user_name"] != DBNull.Value ? dataReader["user_name"].ToString() : string.Empty,
            role_name = dataReader["role_name"] != DBNull.Value ? dataReader["role_name"].ToString() : string.Empty,
            email = dataReader["email"] != DBNull.Value ? dataReader["email"].ToString() : string.Empty,
            type_active = dataReader["type_active"] != DBNull.Value
              ? dataReader["type_active"].ToString()
              : string.Empty
          };
          Result.Add(model);
        }
      }

      if (sqlConnection.State != System.Data.ConnectionState.Closed)
        sqlConnection.Close();
    }
    catch (Exception)
    {
      throw;
    }

    return new DataTableResultModel
    {
      data = Result,
      draw = args.draw,
      recordsTotal = recordsTotal,
      recordsFiltered = recordsTotal,
      error = null
    };
  }

  private int UserCount(DataTableRequestModel args)
  {
    var result = 0;
    try
    {
      using var sqlConnection = _context.DConnection1();
      using var sqlCommand = new SqlCommand(StaticSp.StpUserIndexCount, sqlConnection);
      sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
      sqlCommand.Parameters.AddWithValue("@Keywords", (args.search.value ?? "").Trim());
      sqlCommand.Parameters.AddWithValue("@TypeActive", args.TypeActive);
      sqlConnection.Open();
      using (var dataReader = sqlCommand.ExecuteReader())
      {
        while (dataReader.Read())
        {
          result = (int)dataReader[0];
        }
      }

      sqlConnection.Close();
    }
    catch
    {
      return 0;
    }

    return result;
  }

  public ResponseResultModel Upsert(UserModel model)
  {
    ResponseResultModel resultModel = new();
    using var sqlConnection = _context.DConnection1();
    using var sqlCommand = new SqlCommand(StaticSp.StpUserUpsert, sqlConnection);
    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

    sqlCommand.Parameters.AddWithValue("@Role_ID", model.id_roles);
    sqlCommand.Parameters.AddWithValue("@Full_Name", model.full_name);
    sqlCommand.Parameters.AddWithValue("@User_Name", model.user_name);
    sqlCommand.Parameters.AddWithValue("@Email", model.email);
    sqlCommand.Parameters.AddWithValue("@Password", model.password);

    sqlConnection.Open();

    using (var dataReader = sqlCommand.ExecuteReader())
    {
      while (dataReader.Read())
      {
        resultModel.Success = Convert.ToBoolean(dataReader["Code"]);
        resultModel.Message = dataReader["Message"].ToString();
        resultModel.Payload = dataReader["Payload"].ToString();
      }

      dataReader.Close();
    }

    sqlConnection.Close();

    return resultModel;
  }

  public UserModel GetById(string? id)
  {
    UserModel result = new();

    using var sqlConnection = _context.DConnection1();
    using var sqlCommand = new SqlCommand(StaticSp.StpUserDetail, sqlConnection);
    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

    sqlCommand.Parameters.AddWithValue("@ID", id);

    sqlConnection.Open();

    using (var dataReader = sqlCommand.ExecuteReader())
    {
      while (dataReader.Read())
      {
      }

      dataReader.Close();
    }

    sqlConnection.Close();

    return result;
  }

  public ResponseResultModel Delete(string id)
  {
    ResponseResultModel resultModel = new();

    using var sqlConnection = _context.DConnection1();
    using var sqlCommand = new SqlCommand(StaticSp.StpUserDelete, sqlConnection);
    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

    sqlCommand.Parameters.AddWithValue("@ID", id);

    sqlConnection.Open();

    using (var dataReader = sqlCommand.ExecuteReader())
    {
      while (dataReader.Read())
      {
        resultModel.Success = Convert.ToBoolean(dataReader["Code"]);
        resultModel.Message = dataReader["Message"].ToString();
        resultModel.Payload = dataReader["Payload"].ToString();
      }
      dataReader.Close();
    }
    sqlConnection.Close();

    return resultModel;
  }

  public ResponseResultModel Restore(string id)
  {
    ResponseResultModel resultModel = new();

    using var sqlConnection = _context.DConnection1();
    using var sqlCommand = new SqlCommand(StaticSp.StpUserRestore, sqlConnection);
    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

    sqlCommand.Parameters.AddWithValue("@ID", id);

    sqlConnection.Open();

    using (var dataReader = sqlCommand.ExecuteReader())
    {
      while (dataReader.Read())
      {
        resultModel.Success = Convert.ToBoolean(dataReader["Code"]);
        resultModel.Message = dataReader["Message"].ToString();
        resultModel.Payload = dataReader["Payload"].ToString();
      }
      dataReader.Close();
    }
    sqlConnection.Close();

    return resultModel;
  }
}
