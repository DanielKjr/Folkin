using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DbUnitTest
{
    public DatabaseHandler handler;
    // A Test behaves as an ordinary method
    [Test]
    public void DbUnitTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    private void CreateDBTable()
    {

        var cmd = new SqliteCommand($"CREATE TABLE IF NOT EXISTS User (ID INTEGER PRIMARY KEY, Type STRING)", (SqliteConnection)handler.Connection);
        cmd.ExecuteNonQuery();

        cmd = new SqliteCommand($"CREATE TABLE IF NOT EXISTS Deck ( ID INTEGER PRIMARY KEY, UserID INTEGER,Name STRING, FOREIGN KEY(UserID) REFERENCES User(ID))", (SqliteConnection)handler.Connection);
        cmd.ExecuteNonQuery();


        cmd = new SqliteCommand($"CREATE TABLE IF NOT EXISTS Card" +
           $" (ID INTEGER PRIMARY KEY, DeckID INTEGER, Title STRING, " +
           $"Type STRING, Tag INTEGER, TagText STRING, Description STRING, Icon " +
           $"STRING, Sprite STRING, FOREIGN KEY(DeckID) REFERENCES Deck(ID))", (SqliteConnection)handler.Connection);
        cmd.ExecuteNonQuery();


    }

    [Test]
    public void CanAddCardToDatabase()
    {

        DatabaseHandler.IsUnitTesting = true;
        handler = new DatabaseHandler();
        handler.Open();
        CreateDBTable();


        CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, "TagText", new int[2] { 3, 5 }, "Axe");
        CardManager.Instance.SaveCardToDb(1, card, handler);

        CardData cardFromDB = CardManager.Instance.FindFromDb("TitleText", 1, handler)[0];

        Assert.IsNotNull(cardFromDB);
        Assert.AreEqual("TitleText", cardFromDB.TitleText);
        Assert.AreEqual(1, cardFromDB.DeckID);

        handler.Close();


    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DbUnitTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
