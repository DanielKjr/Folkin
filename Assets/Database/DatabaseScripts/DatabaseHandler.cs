using JetBrains.Annotations;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Rendering;
public enum DatabaseType { Memory, dbFile}
public class DatabaseHandler
{
  
    protected SQLiteDatabaseProvider provider;
    protected IDbConnection connection;
    public CardManager CManager ;
    public DeckManager DManager ;
    public DatabaseType DBType;



    public IDbConnection Connection
    {
        get
        {
            if (connection == null)
            {
                connection = provider.CreateConnection();

            }
            return connection;
        }

    }

    public static bool IsUnitTesting { get; set; }
    public static bool DBIsMade { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateReferences()
    {
        CManager = new CardManager();
        DManager = new DeckManager();
    }
    public void ChangeDatabaseType(DatabaseType dbType)
    {
        switch (dbType)
        {
            case DatabaseType.Memory:
                provider = new SQLiteDatabaseProvider("Data Source=:memory:; Version=3; New=True");
                break;
            case DatabaseType.dbFile:
                provider = new SQLiteDatabaseProvider("Data Source=CardDatabase.db; Version=3; New=False");
                break;
            default:
                break;
        }
     
    }

    /// <summary>
    /// Creates a database if the database file doesn't exist
    /// </summary>
    public void CreateDatabase()
    {
        if (!File.Exists("CardDatabase.db"))
        {
            string sqlConnectionString = "Data Source =CardDatabase.db; new=True";
            var sqlConnection = new SqliteConnection(sqlConnectionString);

            sqlConnection.Open();

            string cmd = File.ReadAllText("CreateCardDb.sql");
            var sqliteDb = new SqliteCommand(cmd, sqlConnection);

            sqliteDb.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }





    public void ChangeDeck(string name)
    {


    }

    public void Close()
    {
        connection.Close();
    }

    public void Open()
    {
        if (connection == null)
        {
            connection = provider.CreateConnection();
        }

        connection.Open();

    }
}
