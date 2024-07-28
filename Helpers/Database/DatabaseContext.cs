using System.Data.SqlClient;

namespace BaseProjectDotnet.Helpers.Database;

public class DatabaseContext
{
    private string Connection1 { get; }

    public DatabaseContext(IConfiguration configuration, string dbConfigName = "ConnectionStrings")
    {
        var database = new DatabaseConnection();
        configuration.Bind(dbConfigName, database);
        Connection1 = database.MainDatabase;
    }
    public SqlConnection DConnection1()
    {
        return new SqlConnection(Connection1);
    }
}

public class DatabaseConnection
{
    public string MainDatabase { get; set; }
}
