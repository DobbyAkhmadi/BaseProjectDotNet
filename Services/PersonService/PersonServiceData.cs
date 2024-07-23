using System.Data;
using System.Data.SqlClient;
using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Models;
using BaseProjectDotnet.Services.PersonService.Model;

namespace BaseProjectDotnet.Services.PersonService;

internal class PersonServiceData(DatabaseContext context) : IPersonService
{
    public DbResponseResult Upsert(List<PersonModel>? model)
    {
        var connection = context.DConnection1();
        var responseResult = new DbResponseResult();

        using var command = new SqlCommand("[dbo].[SP_UpsertPerson]", connection);
        command.CommandType = CommandType.StoredProcedure;
        if (model != null) command.Parameters.Add(UpsertUdt(model));

        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            responseResult.StatusCode = 500;
            responseResult.Message = $"An error occurred: {e.Message}";
        }
        finally
        {
            connection.Close();
        }

        return responseResult;
    }

    private static SqlParameter UpsertUdt(List<PersonModel> model)
    {
        if (model.Count == 0)
        {
            return new SqlParameter("@person", null);
        }

        var tvp = new DataTable();
        tvp.Columns.Add(new DataColumn("ID", typeof(int)));
        tvp.Columns.Add(new DataColumn("ID_Hobby", typeof(int)));
        tvp.Columns.Add(new DataColumn("ID_Gender", typeof(int)));
        tvp.Columns.Add(new DataColumn("Name", typeof(string)));
        tvp.Columns.Add(new DataColumn("Age", typeof(int)));

        foreach (var person in model)
        {
            tvp.Rows.Add(person.Id,
                person.IdHobby,
                person.IdGender,
                person.Name,
                person.Age);
        }

        SqlParameter udt;

        try
        {
            udt = new SqlParameter("@person", SqlDbType.Structured)
            {
                TypeName = "UDT_PERSON",
                Value = tvp
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        if (udt == null)
        {
            throw new ArgumentNullException(nameof(udt));
        }

        return udt;
    }
}