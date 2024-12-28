using Dapper;
using Microsoft.Data.SqlClient;

namespace ETag.Services;

public class UserServices
{
    private readonly SqlConnection connection;

    public UserServices(SqlConnection connection)
    {
        this.connection = connection;
    }

    public async Task<IEnumerable<User>> GetUser()
    {
        var sql = "SELECT ID,Title FROM Tb_Users";
        var products = await connection.QueryAsync<User>(sql);
        return products;
    }

    public async Task<IEnumerable<User>> GetUserByName(string name, int top)
    {
        var sql = "SELECT Top (@top) ID,Title FROM Tb_Users Where Title like '%'+ @name + '%'";
        var products = await connection.QueryAsync<User>(
            sql,
            new
            {
                @top = top,
                @name = name
            });
        return products;
    }

}

public class User
{
    public int ID { get; set; }
    public string Title { get; set; }
}
