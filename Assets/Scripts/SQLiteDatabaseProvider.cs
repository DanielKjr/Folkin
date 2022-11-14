using Mono.Data.Sqlite;
using System.Data;

public class SQLiteDatabaseProvider : IDatabaseProvider
{
    private readonly string connectionString;

    public SQLiteDatabaseProvider(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(connectionString);
    }
}
