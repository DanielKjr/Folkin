using Mono.Data.Sqlite;
using System.Data;

public class DatabaseProvider : IDatabaseProvider
{
    private readonly string connectionString;

    public DatabaseProvider(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(connectionString);
    }
}
