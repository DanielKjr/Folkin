using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class DatabaseHandler 
{
    protected SQLiteDatabaseProvider provider;
    protected IDbConnection connection;

    public DatabaseHandler()
    {
        provider = new SQLiteDatabaseProvider("Data Source=CardDatabase.db; Version=3; new=False");
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

    public void LoadCards()
    {
        Open();

        var cmd = new SqliteCommand("SELECT All FROM ");
    }

    public void ChangeDeck(string name)
    {
        Open();

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
