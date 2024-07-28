using System.Data.SqlClient;
using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Helpers.Database.Procedure;
using BaseProjectDotnet.Helpers.Global.Models;
using BaseProjectDotnet.Services.AuditService.Model;

namespace BaseProjectDotnet.Services.AuditService;

public class AuditTrailServiceData(DatabaseContext _context) : IAuditTrailService
{
  public DataTableResultModel Index(DataTableRequestModel args)
  {
    List<AuditTrailModel> Result = [];
    var recordsTotal = AuditCount(args);
    try
    {
      using var sqlConnection = _context.DConnection1();
      using var sqlCommand = new SqlCommand(StaticSp.StpAuditTrailIndex, sqlConnection);
      sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
      sqlCommand.Parameters.AddWithValue("@Keywords", (args.search.value ?? "").Trim());
      sqlCommand.Parameters.AddWithValue("@FromStart", args.start);
      sqlCommand.Parameters.AddWithValue("@FromEnd", args.length);
      sqlCommand.Parameters.AddWithValue("@Sort", args.order[0]?.dir);
      sqlCommand.Parameters.AddWithValue("@FieldIndex", args.order[0].column);
      sqlConnection.Open();

      using (var dataReader = sqlCommand.ExecuteReader())
      {
        while (dataReader.Read())
        {
          var model = new AuditTrailModel()
          {
            id = dataReader["id"] != DBNull.Value ? dataReader["id"].ToString() : string.Empty,
            user_id = dataReader["user_id"] != DBNull.Value ? dataReader["user_id"].ToString() : string.Empty,
            user_name = dataReader["user_name"] != DBNull.Value ? dataReader["user_name"].ToString() : string.Empty,
            role_id = dataReader["role_id"] != DBNull.Value ? dataReader["role_id"].ToString() : string.Empty,
            role_name = dataReader["role_name"] != DBNull.Value ? dataReader["role_name"].ToString() : string.Empty,
            remote_ip = dataReader["remote_ip"] != DBNull.Value ? dataReader["remote_ip"].ToString() : string.Empty,
            session_id = dataReader["session_id"] != DBNull.Value ? dataReader["session_id"].ToString() : string.Empty,
            action = dataReader["action"] != DBNull.Value ? dataReader["action"].ToString() : string.Empty,
            function_name = dataReader["function_name"] != DBNull.Value
              ? dataReader["function_name"].ToString()
              : string.Empty,
            message = dataReader["message"] != DBNull.Value ? dataReader["message"].ToString() : string.Empty,
            old_model = dataReader["old_model"] != DBNull.Value ? dataReader["old_model"].ToString() : string.Empty,
            new_model = dataReader["new_model"] != DBNull.Value ? dataReader["new_model"].ToString() : string.Empty,
            latency = dataReader["latency"] != DBNull.Value ? dataReader["latency"].ToString() : string.Empty,
            created_date = dataReader["created_date"] != DBNull.Value
              ? Convert.ToDateTime(dataReader["created_date"])
              : new DateTime()
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

    return new DataTableResultModel()
    {
      data = Result,
      draw = args.draw,
      recordsTotal = recordsTotal,
      recordsFiltered = recordsTotal,
      error = null
    };
}

  private int AuditCount(DataTableRequestModel args)
  {
    var result = 0;
    try
    {
      using var sqlConnection = _context.DConnection1();
      using var sqlCommand = new SqlCommand(StaticSp.StpAuditTrailIndexCount, sqlConnection);
      sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
      sqlCommand.Parameters.AddWithValue("@Keywords", (args.search.value ?? "").Trim());
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

  public DbResponseResult SaveAudit(AuditTrailModel auditTrailModel)
  {
    return null;
  }

  public AuditTrailModel GetById(string id)
  {
    AuditTrailModel Result = new();

    using var sqlConnection = _context.DConnection1();
    using var sqlCommand = new SqlCommand(StaticSp.StpAuditTrailDetail, sqlConnection);
    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

    sqlCommand.Parameters.AddWithValue("@ID", id);

    sqlConnection.Open();

    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
    {
      while (dataReader.Read())
      {
        var model = new AuditTrailModel()
        {
          id = dataReader["id"] != DBNull.Value ? dataReader["id"].ToString() : string.Empty,
          user_id = dataReader["user_id"] != DBNull.Value ? dataReader["user_id"].ToString() : string.Empty,
          user_name = dataReader["user_name"] != DBNull.Value ? dataReader["user_name"].ToString() : string.Empty,
          role_id = dataReader["role_id"] != DBNull.Value ? dataReader["role_id"].ToString() : string.Empty,
          role_name = dataReader["role_name"] != DBNull.Value ? dataReader["role_name"].ToString() : string.Empty,
          remote_ip = dataReader["remote_ip"] != DBNull.Value ? dataReader["remote_ip"].ToString() : string.Empty,
          session_id = dataReader["session_id"] != DBNull.Value ? dataReader["session_id"].ToString() : string.Empty,
          action = dataReader["action"] != DBNull.Value ? dataReader["action"].ToString() : string.Empty,
          function_name = dataReader["function_name"] != DBNull.Value
            ? dataReader["function_name"].ToString()
            : string.Empty,
          message = dataReader["message"] != DBNull.Value ? dataReader["message"].ToString() : string.Empty,
          old_model = dataReader["old_model"] != DBNull.Value ? dataReader["old_model"].ToString() : string.Empty,
          new_model = dataReader["new_model"] != DBNull.Value ? dataReader["new_model"].ToString() : string.Empty,
          latency = dataReader["latency"] != DBNull.Value ? dataReader["latency"].ToString() : string.Empty,
          created_date = dataReader["created_date"] != DBNull.Value
            ? Convert.ToDateTime(dataReader["created_date"])
            : new DateTime()
        };
        Result = model;
      }

      dataReader.Close();
    }

    sqlConnection.Close();

    return Result;
  }

  public DbResponseResult Archived(string id)
  {
    try
    {
      DbResponseResult Result = new();

      using var sqlConnection = _context.DConnection1();
      using var sqlCommand = new SqlCommand(StaticSp.StpAuditTrailArchived, sqlConnection);
      sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

      sqlCommand.Parameters.AddWithValue("@ID", id);

      sqlConnection.Open();

      using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
      {
        while (dataReader.Read())
        {
          Result.Success = Convert.ToBoolean(dataReader["Kode"]);
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

  public DbResponseResult Restore(string id)
  {
    try
    {
      DbResponseResult Result = new();

      using var sqlConnection = _context.DConnection1();
      using var sqlCommand = new SqlCommand(StaticSp.StpAuditTrailRestore, sqlConnection);
      sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

      sqlCommand.Parameters.AddWithValue("@ID", id);

      sqlConnection.Open();

      using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
      {
        while (dataReader.Read())
        {
          Result.Success = Convert.ToBoolean(dataReader["Kode"]);
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
