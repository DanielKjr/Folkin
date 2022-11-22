using Mono.Cecil.Cil;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEditor.MemoryProfiler;
using UnityEngine.Rendering;

public class CardRepository : ICardRepository
{
    private readonly IDatabaseProvider provider;
    private readonly ICardMapper mapper;
    private IDbConnection connection;

    public CardRepository(IDatabaseProvider provider, ICardMapper mapper)
    {
        this.provider = provider;
        this.mapper = mapper;
    }

    public string IconArrayToString(CardData card)
    {
        string iconValuesToString = string.Empty;
        int count = 0;
        for (int i = 0; i < card.IconValues.Length; i++)
        {
            iconValuesToString += card.IconValues[i];
            count++;
            if (count != card.IconValues.Length)
            {
                iconValuesToString += ",";
            }

        }

        return iconValuesToString;
    }

    public void AddCard(int deckId, CardData card)
    {
        var cmd = new SqliteCommand($"INSERT INTO Card (ID, DeckID, Title, Type, Tag, TagText, Description, Icon, Sprite) VALUES " +
           $"(null, " +
           $" '{deckId}'," +
           $" '{card.TitleText}', " +
           $" '{card.TypeText}'," +
           $" '{(int)card.TType}', " +
           $" '{card.TagText}'," +
           $" '{card.DescriptionText}'," +
           $" '{IconArrayToString(card)}'," +
           $" '{card.SpritePath}')",
           (SqliteConnection)connection);

        cmd.ExecuteNonQuery();
    }

    public void AddDeck(int userId, Deck deck)
    {
        var cmd = new SqliteCommand($"DELETE FROM Card WHERE DeckID='{deck.ID}'", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();

        cmd = new SqliteCommand($"DELETE FROM Deck WHERE ID='{deck.ID}'", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();

        cmd = new SqliteCommand($"INSERT INTO Deck (ID, UserID, Name) VALUES (null, '{userId}', '{deck.Name}')", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();

        foreach (CardData card in deck.CardDatas)
        {
            AddCard(deck.ID, card);
        }
    }


    public void DeleteCard(string name)
    {
        var cmd = new SqliteCommand($"DELETE FROM Card WHERE Title='{name}'", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();
    }

    public void DeleteCard(CardData card)
    {
        var cmd = new SqliteCommand($"DELETE FROM Card WHERE Title='{card.TitleText}'", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();
    }

    public void EditCard(int deckId, string cardName, CardData card)
    {
        var cmd = new SqliteCommand($"DELETE FROM Card WHERE DeckID='{deckId}' AND Title='{cardName}'", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();
        //TODO HER
        cmd = new SqliteCommand($"INSERT INTO Card (ID, DeckID, Title, Type, Tag, TagText, Description, Icon, Sprite) VALUES " +
          $"(null, " +
          $" '{deckId}', " +
          $" '{card.TitleText}', " +
          $" '{card.TypeText}'," +
          $" '{(int)card.TType}', " +
          $" '{card.TagText}'," +
          $" '{card.DescriptionText}'," +
          $" '{IconArrayToString(card)}'," +
          $" '{card.SpritePath}')",
          (SqliteConnection)connection);

        cmd.ExecuteNonQuery();
    }

    public CardData FindCard(string name)
    {
        var cmd = new SqliteCommand($"SELECT * FROM Card WHERE Title='{name}'",
            (SqliteConnection)connection);

        var dataRead = cmd.ExecuteReader();

        var result = mapper.MapCardsFromReader(dataRead).FirstOrDefault();




        return result;
    }

    public CardData FindCard(CardData card)
    {

        var cmd = new SqliteCommand($"SELECT * FROM Card WHERE Title='{card.TitleText}'",
            (SqliteConnection)connection);

        var dataRead = cmd.ExecuteReader();
        var result = mapper.MapCardsFromReader(dataRead).FirstOrDefault();

        return result;


    }

    public List<CardData> GetAllCards()
    {


        var cmd = new SqliteCommand($"SELECT * FROM Card", (SqliteConnection)connection);
        var reader = cmd.ExecuteReader();

        var result = mapper.MapCardsFromReader(reader);

        return result;
    }

    public List<CardData> GetAllCards(int deckId)
    {
        var cmd = new SqliteCommand($"SELECT * FROM Card WHERE DeckID='{deckId}'", (SqliteConnection)connection);
        var reader = cmd.ExecuteReader();

        var result = mapper.MapCardsFromReader(reader);

        return result;
    }


    private void CreateDBTables()
    {
        var cmd = new SqliteCommand($"CREATE TABLE IF NOT EXISTS User (ID INTEGER PRIMARY KEY, Type STRING)", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();

        cmd = new SqliteCommand($"CREATE TABLE IF NOT EXISTS Deck ( ID INTEGER PRIMARY KEY, UserID INTEGER,Name STRING, FOREIGN KEY(UserID) REFERENCES User(ID))", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();


        cmd = new SqliteCommand($"CREATE TABLE IF NOT EXISTS Card" +
           $" (ID INTEGER PRIMARY KEY, DeckID INTEGER, Title STRING, " +
           $"Type STRING, Tag INTEGER, TagText STRING, Description STRING, Icon " +
           $"STRING, Sprite STRING, FOREIGN KEY(DeckID) REFERENCES Deck(ID))", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();

    }

    public Deck FindDeck(string name)
    {
        Deck deck = new Deck();


        var cmd = new SqliteCommand($"SELECT Name, UserID FROM Deck WHERE Name='{name}'", (SqliteConnection)connection);
        var dataRead = cmd.ExecuteReader();

        while (dataRead.Read())
        {
            deck.Name = dataRead.GetString(0);
            deck.ID = dataRead.GetInt32(1);
        }

        cmd = new SqliteCommand($"SELECT * FROM Card WHERE DeckID='{deck.ID}'", (SqliteConnection)connection);
        dataRead = cmd.ExecuteReader();

        deck.CardDatas = mapper.MapCardsFromReader(dataRead);
        return deck;

    }

    public Deck FindDeck(Deck deck)
    {

        var cmd = new SqliteCommand($"SELECT Name, UserID FROM Deck WHERE Name='{deck.Name}'", (SqliteConnection)connection);
        var dataRead = cmd.ExecuteReader();

        while (dataRead.Read())
        {
            deck.Name = dataRead.GetString(0);
            deck.ID = dataRead.GetInt32(1);
        }

        cmd = new SqliteCommand($"SELECT * FROM Card WHERE DeckID='{deck.ID}'", (SqliteConnection)connection);
        dataRead = cmd.ExecuteReader();

        deck.CardDatas = mapper.MapCardsFromReader(dataRead);



        return deck;
    }


    public void Open()
    {
        if (connection == null)
        {
            connection = provider.CreateConnection();
        }
        connection.Open();
        CreateDBTables();
    }
    public void Close()
    {
        connection.Close();
    }
}

