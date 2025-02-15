using System.Text.Json;
using Dapper;
using ETL.Core.Interfaces;
using Microsoft.Data.SqlClient;

namespace ETL.Outputs;

public class SqlDatabaseOutput : IDataOutputProvider
{
    public async Task LoadDataAsync(IEnumerable<dynamic> data, string configJson)
    {
        var config = JsonSerializer.Deserialize<SqlOutputConfig>(configJson);
        using var connection = new SqlConnection(config.ConnectionString);
        await connection.ExecuteAsync(config.InsertQuery, data);
    }
}

public class SqlOutputConfig
{
    public string ConnectionString { get; set; }
    public string InsertQuery { get; set; }
}