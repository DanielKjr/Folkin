using Mono.Data.Sqlite;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.U2D.Common;

public class CardManager : DatabaseHandler
{
    private static CardManager instance;
    private Card currentCard;

    public static CardManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CardManager();
            }
            return instance;
        }
    }

    //Search functions

    /// <summary>
    /// Returns a Card object that matches the Name of the card and the Deck ID
    /// </summary>
    /// <param name="cardName"></param>
    /// <param name="deckID"></param>
    /// <returns></returns>
    public Card FindFromDb(string cardName, int deckID)
    {
        Card card;
        Open();

        var cmd = new SqliteCommand($"SELECT Title, Type, Tag, TagText, Description, Icon, Sprite " +
            $"FROM Card" +
            $" WHERE DeckID='{deckID}' " +
            $"AND Title='{cardName}'",
            (SqliteConnection)connection);

        var dataRead = cmd.ExecuteReader();

        //retrieve all columns from database
        string title = dataRead.GetString(0);
        string type = dataRead.GetString(1);
        int tag = dataRead.GetInt32(2);
        string tagText = dataRead.GetString(3);
        string description = dataRead.GetString(4);
        string icon = dataRead.GetString(5);
        string sprite = dataRead.GetString(6);

        //divide into strings using commas
        string[] iconSplit = icon.Split(',');
        int[] iconValues = new int[iconSplit.Length];


        for (int i = 0; i < iconSplit.Length; i++)
        {
            //Convert to integer
            iconValues[i] = int.Parse(iconSplit[i]);
        }

         card = new Card(title, description, type, (TagType)tag, tagText, iconValues, sprite) { SpritePath = sprite, iconValues = iconValues };

        Close();
        return card;
    }
    public List<CardData> FindFromDb(string cardName, int deckID, SqliteConnection sqlConnection)
    {
        List<CardData> cards = new List<CardData>();
     // sqlConnection.Open();

        //var cmd = new SqliteCommand($"SELECT Title, Type, Tag, TagText, Description, Icon, Sprite " +
        //    $"FROM Card" +
        //    $"WHERE DeckID = '{deckID}' " +
        //    $"AND Title = '{cardName}'",
        //    (SqliteConnection)DatabaseHandler.Instance.Connection);


        var cmd = new SqliteCommand($"SELECT * FROM Card WHERE DeckID={deckID}",(SqliteConnection)DatabaseHandler.Instance.Connection);

        var dataRead = cmd.ExecuteReader();

        //retrieve all columns from database

        while (dataRead.Read())
        {
            var id = dataRead.GetInt32(0);
            var deckId = dataRead.GetInt32(1);
            var title = dataRead.GetString(2);
            var type = dataRead.GetString(3);
            var tag = dataRead.GetInt32(4);
            var tagText = dataRead.GetString(5);
            var description = dataRead.GetString(6);
            var icon = dataRead.GetString(7);
            var sprite = dataRead.GetString(8);

            //divide into strings using commas
            string[] iconSplit = icon.Split(',');
            int[] iconValues = new int[iconSplit.Length];


            for (int i = 0; i < iconSplit.Length; i++)
            {
                //Convert to integer
                iconValues[i] = int.Parse(iconSplit[i]);
            }
           CardData card = new CardData(title, description, type, (TagType)tag, tagText, iconValues, sprite) { DeckID = deckId };
         //   Card card = new Card();
          //  card.SetCard(title, type, 1, description, tagText, iconValues, tagText);
            cards.Add(card);
        }
      //  sqlConnection.Close();
        return cards;
    }


    /// <summary>
    /// Returns a card from the current deck that matches the name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="deck"></param>
    /// <returns></returns>
    //public Card FindFromDeck(string name, Deck deck)
    //{

    //    //Card? card = deck.cards.Find(x => x.Name == name);

    //    return card;


    //}

    //Edit functions

    /// <summary>
    /// Deletes a card from the matching Deck and inserts a new one.
    /// </summary>
    /// <param name="deckID"></param>
    /// <param name="cardName"></param>
    /// <param name="updatedCard"></param>
    public void EditCard(int deckID, string cardName, Card updatedCard)
    {
        Open();

        var cmd = new SqliteCommand($"DELETE FROM Card WHERE DeckID={deckID} AND Name={cardName}", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();

        cmd = new SqliteCommand($"INSERT INTO Card VALUES " +
            $"('null'," +
            $"'{deckID}'," +
            $"'{updatedCard.titleText}'," +
            $"'{updatedCard.typeText}'," +
            $"'{(int)updatedCard.TType}', " +
            $"'{updatedCard.tagText}', " +
            $"'{updatedCard.descriptionText}'," +
            $"'{updatedCard.iconValues}'," +
            $"'{updatedCard.SpritePath}'",
            (SqliteConnection)connection);

        cmd.ExecuteNonQuery();

        Close();
    }

    //SaveFunction

    /// <summary>
    /// Saves card to the database with the matching deck
    /// </summary>
    /// <param name="deckId"></param>
    /// <param name="card"></param>
    public void SaveCardToDb(int deckId, Card card, Deck de)
    {
       // Open();

        var cmd = new SqliteCommand($"INSERT INTO Card (ID, DeckID, Title, Type, Tag, TagText, Description, Icon, Sprite) VALUES " +
            $"('null', " +
            $" '{deckId}', " +
            $" '{card.titleText}', " +
            $" '{card.typeText}'," +
            $" '{(int)card.TType}', " +
            $" '{card.tagText}'," +
            $" '{card.descriptionText}'," +
            $" '{card.iconValues}'," +
            $" '{card.SpritePath}')",
            (SqliteConnection)connection);

        cmd.ExecuteNonQuery();

      //  Close();

    }
    public void SaveCardToDb(int deckId, Card card, SqliteConnection sqlconnection)
    {
         sqlconnection.Open();

        var cmd = new SqliteCommand($"INSERT INTO Card (ID, DeckID, Title, Type, Tag, TagText, Description, Icon, Sprite) VALUES " +
            $"(null, " +
            $" '{deckId}', " +
            $" '{card.titleText}', " +
            $" '{card.typeText}'," +
            $" '{(int)card.TType}', " +
            $" '{card.tagText}'," +
            $" '{card.descriptionText}'," +
            $" '{card.iconValues}'," +
            $" '{card.SpritePath}')",
            sqlconnection);

        cmd.ExecuteNonQuery();

        sqlconnection.Close();

    }


    //Delete functions

    /// <summary>
    /// Deletes all cards in database with the given name. 
    /// </summary>
    /// <param name="name"></param>
    public void DeleteFromDatabase(string name)
    {
        Open();

        var cmd = new SqliteCommand($"DELETE FROM Card WHERE Title='{name}'", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();


        Close();

    }

    /// <summary>
    /// Deletes all cards in database that matches the title of the card passed through the parameter.
    /// </summary>
    /// <param name="card"></param>
    public void DeleteFromDatabase(Card card)
    {
        Open();

        var cmd = new SqliteCommand($"DELETE FROM Card WHERE Title='{card.titleText}'", (SqliteConnection)connection);
        cmd.ExecuteNonQuery();

        Close();
    }

}
