using Mono.Data.Sqlite;
using System.Data;

public class DatabaseProvider : IDatabaseProvider
{
    private readonly string connectionString;

    /// <summary>
    /// Creates an Sqliteconnection through this string, atlernatively use "Data Source=:memory:" 
    /// </summary>
    /// <param name="connectionString"></param>
    public DatabaseProvider(string connectionString)
    {
        this.connectionString = connectionString;
    }
    ///<inheritdoc />
    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(connectionString);
    }
}
