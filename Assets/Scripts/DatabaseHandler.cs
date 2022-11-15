using JetBrains.Annotations;
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

    public List<Card> LoadCards(int ID)
    {
        Deck deck = new Deck();
        Open();

        var cmd = new SqliteCommand($"SELECT Title, Type, Tag, Description, Icon FROM 'Card' WHERE DeckID={ID}", (SqliteConnection)connection);
        var dataRead = cmd.ExecuteReader();

        while (dataRead.Read())
        {
            string title = dataRead.GetString(0);
            string type = dataRead.GetString(1);
            int tag = dataRead.GetInt32(2);
            string description = dataRead.GetString(3);
            int icon = dataRead.GetInt32(4);

            //add a new card to the list on the Deck object using this information
        }


        Close();
        return deck.cards;

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
