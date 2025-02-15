using System.Text.Json;
using Dapper;
using ETL.Core.Interfaces;
using Microsoft.Data.SqlClient;

namespace ETL.Database;

public class SqlDataSource : IDataSourceProvider
{
    public async Task<IEnumerable<dynamic>> ExtractDataAsync(string configJson)
    {
        var config = JsonSerializer.Deserialize<SqlConfig>(configJson);
        using var connection = new SqlConnection(config.ConnectionString);
        return await connection.QueryAsync<dynamic>(config.Query);
    }
}

public class SqlConfig
{
    public string ConnectionString { get; set; }
    public string Query { get; set; }
}