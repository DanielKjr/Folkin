using JetBrains.Annotations;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Rendering;

public class DatabaseHandler
{
    protected SQLiteDatabaseProvider provider = new SQLiteDatabaseProvider("Data Source=CardDatabase.db; New=False");
    protected IDbConnection connection;
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

    private static DatabaseHandler instance;
    public static DatabaseHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DatabaseHandler();
            }
            return instance;

        }
    }


    public DatabaseHandler()
    {
        //  provider = new SQLiteDatabaseProvider("Data Source=CardDatabase.db; Version=3; new=False");
    }
    public DatabaseHandler(string memoryString)
    {
        provider = new SQLiteDatabaseProvider(memoryString);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

