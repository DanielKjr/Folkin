using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.Plastic.Antlr3.Runtime.Tree;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.U2D.Common;

public class CardManager : DatabaseHandler
{

    private Card currentCard;

    

    //Search functions

    /// <summary>
    /// Returns a Card object that matches the Name of the card and the Deck ID
    /// </summary>
    /// <param name="cardName"></param>
    /// <param name="deckID"></param>
    /// <returns></returns>
    public List<CardData> FindFromDb(string cardName, int deckID)
    {
        List<CardData> cards = new List<CardData>();
        //Open();

        var cmd = new SqliteCommand($"SELECT * FROM Card" +
            $" WHERE DeckID='{deckID}' " +
            $"AND Title='{cardName}'",
            (SqliteConnection)connection);

        var dataRead = cmd.ExecuteReader();

        while (dataRead.Read())
        {

            //retrieve all columns from database
            //var id = dataRead.GetInt32(0);
            //var deckId = dataRead.GetInt32(1);
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
                if (char.IsDigit(iconSplit[i][i]))
                {
                    iconValues[i] = int.Parse(iconSplit[i]);
                }

            }

            cards.Add(new CardData(title, description, type, (TagType)tag, tagText, iconValues, sprite) { DeckID = deckID });
        }



        return cards;
    }


    /// <summary>
    /// Overload used by the unit tests
    /// </summary>
    /// <param name="cardName"></param>
    /// <param name="deckID"></param>
    /// <param name="handler"></param>
    /// <returns></returns>
    public List<CardData> FindFromDb(string cardName, int deckID, DatabaseHandler handler)
    {
        List<CardData> cards = new List<CardData>();

        var cmd = new SqliteCommand($"SELECT * FROM Card" +
            $" WHERE DeckID='{deckID}' " +
            $"AND Title='{cardName}'",
            (SqliteConnection)handler.Connection);

        var dataRead = cmd.ExecuteReader();

        while (dataRead.Read())
        {

            //retrieve all columns from database
            //var id = dataRead.GetInt32(0);
            //var deckId = dataRead.GetInt32(1);
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
                if (char.IsDigit(iconSplit[i][i]))
                {
                    iconValues[i] = int.Parse(iconSplit[i]);
                }
               
            }

            cards.Add(new CardData(title, description, type, (TagType)tag, tagText, iconValues, sprite) { DeckID = deckID });
        }



        return cards;
    }

    /// <summary>
    /// Returns a card from the current deck that matches the name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="deck"></param>
    /// <returns></returns>
    public Card FindFromDeck(string name, Deck deck)
    {

        Card? card = deck.cards.Find(x => x.name == name);

        return card;


    }

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
    /// May not work
    /// Saves card to the database with the matching deck
    /// </summary>
    /// <param name="deckId"></param>
    /// <param name="card"></param>
    public void SaveCardToDb(int deckId, CardData card)
    {

        var cmd = new SqliteCommand($"INSERT INTO Card (ID, DeckID, Title, Type, Tag, TagText, Description, Icon, Sprite) VALUES " +
             $"(null, " +
             $" '{deckId}', " +
             $" '{card.TitleText}', " +
             $" '{card.TypeText}'," +
             $" '{(int)card.TType}', " +
             $" '{card.TagText}'," +
             $" '{card.DescriptionText}'," +
             $" '{card.IconValues}'," +
             $" '{card.SpritePath}')",
             (SqliteConnection)Connection);


        cmd.ExecuteNonQuery();


    }


    /// <summary>
    /// Overload used for unit tests
    /// </summary>
    /// <param name="deckId"></param>
    /// <param name="card"></param>
    /// <param name="handler"></param>
    public void SaveCardToDb(int deckId, CardData card, DatabaseHandler handler)
    {

        string iconString = string.Empty;
        //TODO make icons to a string in db and split it up to an int in the code 
        var cmd = new SqliteCommand($"INSERT INTO Card (ID, DeckID, Title, Type, Tag, TagText, Description, Icon, Sprite) VALUES " +
            $"(null, " +
            $" '{deckId}', " +
            $" '{card.TitleText}', " +
            $" '{card.TypeText}'," +
            $" '{(int)card.TType}', " +
            $" '{card.TagText}'," +
            $" '{card.DescriptionText}'," +
            $" '{card.IconValues}'," +
            $" '{card.SpritePath}')",
            (SqliteConnection)handler.Connection);

       
        cmd.ExecuteNonQuery();


    }



    //Delete functions

    /// <summary>
    /// NOT USABLE AT THE CURRENT TIME
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
    /// NOT USABLE AT THE CURRENT TIME
    /// Deletes all cards in database that matches the title of the card passed through the parameter.
    /// </summary>
    /// <param name="card"></param>
    public void DeleteFromDatabase(Card card, DatabaseHandler handler)
    {
        Open();

        var cmd = new SqliteCommand($"DELETE FROM Card WHERE Title='{card.titleText}'", (SqliteConnection)handler.Connection);
        cmd.ExecuteNonQuery();

        Close();
    }

}
