using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace TDJakes.DataAccess;

public class MySqlDbAccess : IDbAccess
{
    private readonly IConfiguration _configuration;

    public MySqlDbAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<T>> Fetch<T, U>(
        string storedProcedure,
        U parameters,
        CommandType commandType = CommandType.Text,
        string connectionId = "Default"
        )
    {
        using IDbConnection connection = new MySqlConnection(_configuration.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: commandType);
    }

    public async Task SavaData<T>(string storedProcedure,
        T parameters,
        CommandType commandType = CommandType.Text,
        string connectionId = "Default")
    {
        using IDbConnection connection = new MySqlConnection(_configuration.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, parameters, commandType: commandType);
    }
}
