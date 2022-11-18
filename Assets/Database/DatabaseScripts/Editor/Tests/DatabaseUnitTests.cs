using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Mono.Data.Sqlite;



public class DatabaseUnitTests
{
    public DatabaseHandler handler;

    // A Test behaves as an ordinary method
    [Test]
    public void DatabaseUnitTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    private void CreateDBTable()
    {
        DatabaseHandler.Instance.Open();
        var cmd = new SqliteCommand($"CREATE TABLE IF NOT EXISTS User (ID INTEGER PRIMARY KEY, Type STRING)", (SqliteConnection)DatabaseHandler.Instance.Connection);
        cmd.ExecuteNonQuery();

        cmd = new SqliteCommand($"CREATE TABLE IF NOT EXISTS Deck ( ID INTEGER PRIMARY KEY, UserID INTEGER,Name STRING, FOREIGN KEY(UserID) REFERENCES User(ID))", (SqliteConnection)DatabaseHandler.Instance.Connection);
        cmd.ExecuteNonQuery();


        cmd = new SqliteCommand($"CREATE TABLE IF NOT EXISTS Card" +
           $" (ID INTEGER PRIMARY KEY, DeckID INTEGER, Title STRING, " +
           $"Type STRING, Tag INTEGER, TagText STRING, Description STRING, Icon " +
           $"STRING, Sprite STRING, FOREIGN KEY(DeckID) REFERENCES Deck(ID))", (SqliteConnection)DatabaseHandler.Instance.Connection);
        cmd.ExecuteNonQuery();

        DatabaseHandler.Instance.Close();
    }

    //[Test]
    //public void CanCreateDatabase()
    //{

    //    string sqlConnectionString = "Data Source =CardDatabase.db; new=True";
    //    var sqlConnection = new SqliteConnection(sqlConnectionString);

    //    sqlConnection.Open();

    //    string cmd = File.ReadAllText("CreateCardDb.sql");
    //    var sqliteDb = new SqliteCommand(cmd, sqlConnection);

    //    sqliteDb.ExecuteNonQuery();

    //    sqlConnection.Close();

    //    Assert.AreEqual(File.Exists("CardDatabase.db"), true);

    //}

    [Test]
    public void CardCanBeAdded()
    {

        // string sqlConnectionString = "Data Source =:memory:; new=True";
        //var sqlConnection = new SqliteConnection(sqlConnectionString);
        
       // handler = new DatabaseHandler("Data Source=:memory:; Version=3; New=True");
        
        CreateDBTable();
        DatabaseHandler.Instance.Open();

        //  card.AddComponent(new Card("Title", "Description", "CardType", TagType.ITEM, "TagText", new int[2] { 1, 2 }, "Axe"));

        // Card card = new Card("Title", "Description", "CardType", TagType.ITEM, "TagText", new int[2] { 1, 2 }, "Axe");
        //Card newCard = card.GetComponent<Card>() as Card;
        //newCard.titleText.text = "Title";
        //newCard.descriptionText.text = "Description";
        //newCard.tagText.text = "TagText";
        //newCard.CType = CardType.SKILLCARD;
        //newCard.TType = TagType.SKILL;
        //newCard.iconValues = new int[2] { 2, 3 };
        //newCard.SpritePath = "Axe";
        //  newCard.SetCard("Title", "TypeText", CardType.BASECARD, "DescriptionText", "TagText", new GameObject[2]{ 2,3} , "Axe");

        //  CardManager.Instance.SaveCardToDb(1, card, (SqliteConnection)CardManager.Instance.Connection);


        List<CardData> dbCard = CardManager.Instance.FindFromDb("TitleText", 1, (SqliteConnection)DatabaseHandler.Instance.Connection);
        
        DatabaseHandler.Instance.Close();
        //  Assert.IsNotNull(cardFromDb);
        // Assert.AreEqual("Title", dbCard.titleText);
        Assert.IsNotNull(dbCard[0]);
        Assert.AreEqual("TitleText", dbCard[0].TitleText);
        Assert.AreEqual(1, dbCard[0].DeckID);
        //   Assert.AreEqual(card.titleText, cardFromDb.titleText);

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DatabaseUnitTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
