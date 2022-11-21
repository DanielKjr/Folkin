using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DbUnitTest
{
    private DatabaseHandler handler;


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
    public void CardCanBeFound()
    {
        ICardRepository repository;

        //Arrange
        var mapper = new CardMapper();
        var provider = new SQLiteDatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        //Act
        CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, "TagText", new int[2] { 3, 5 }, "Axe");
        repository.AddCard(1, card);

        var result = repository.FindCard(card);


        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("TitleText", result.TitleText);
        Assert.AreEqual(TagType.ITEM, result.TType);
        repository.Close();
    }
    [Test]
    public void AllCardsCanBeFound()
    {
        ICardRepository repository;

        var mapper = new CardMapper();
        var provider = new SQLiteDatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData[] cards = new CardData[3];
        cards[0] = new CardData("Axe", "Damage", "Action", TagType.ITEM, "ChopChop", new int[2] { 3, 5 }, "Axe");
        cards[1] = new CardData("Sword", "Damage", "CardType", TagType.ITEM, "Poke", new int[2] { 3, 5 }, "Sword");
        cards[2] = new CardData("Potion", "Heal", "CardType", TagType.ITEM, "Chug", new int[2] { 3, 5 }, "Potion");

        for (int i = 0; i < cards.Length; i++)
        {
            repository.AddCard(1, cards[i]);
        }

        var result = repository.GetAllCards();

        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("Axe", result[0].SpritePath);
        Assert.AreEqual("Sword", result[1].SpritePath);
        Assert.AreEqual("Potion", result[2].SpritePath);

        repository.Close();

    }

    [Test]
    public void CanFindDeck()
    {
        ICardRepository repository;

        var mapper = new CardMapper();
        var provider = new SQLiteDatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();
        Deck deck = new Deck();
        deck.ID = 2;
        deck.Name = "TestDeck";
        CardData[] cards = new CardData[3];
        cards[0] = new CardData("Axe", "Damage", "Action", TagType.ITEM, "ChopChop", new int[2] { 3, 5 }, "Axe");
        cards[1] = new CardData("Sword", "Damage", "CardType", TagType.ITEM, "Poke", new int[2] { 3, 5 }, "Sword");
        cards[2] = new CardData("Potion", "Heal", "CardType", TagType.ITEM, "Chug", new int[2] { 3, 5 }, "Potion");

        for (int i = 0; i < cards.Length; i++)
        {
            deck.CardDatas.Add(cards[i]);
  
        }

        repository.AddDeck(2, deck);

        var result = repository.FindDeck(deck);
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.CardDatas.Count);
        Assert.AreEqual(2, result.CardDatas[1].DeckID);
        repository.Close();
    }

    //[Test]
    //public void CanAddCardToDatabase()
    //{
    //    //Arrange
    //    handler = new DatabaseHandler();
    //    handler.CreateReferences();
    //    handler.ChangeDatabaseType(DatabaseType.Memory);

    //    handler.Open();
    //    CreateDBTable();

    //    //Act
    //    CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, "TagText", new int[2] { 3, 5 }, "Axe");
    //    handler.CManager.SaveCardToDb(1, card, handler);

    //    CardData cardFromDB = handler.CManager.FindFromDb("TitleText", 1, handler)[0];
    //    handler.Close();

    //    //Assert
    //    Assert.IsNotNull(cardFromDB);
    //    Assert.AreEqual("TitleText", cardFromDB.TitleText);
    //    Assert.AreEqual(1, cardFromDB.DeckID);




    //}

    //[Test]
    //public void CanLoadDeck()
    //{
    //    Arrange
    //    handler = new DatabaseHandler();
    //    handler.CreateReferences();
    //    handler.ChangeDatabaseType(DatabaseType.Memory);

    //    handler.Open();
    //    CreateDBTable();

    //    Act
    //    CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, "TagText", new int[2] { 3, 5 }, "Axe");

    //    for (int i = 0; i < 10; i++)
    //    {
    //        handler.CManager.SaveCardToDb(1, card, handler);
    //    }

    //    Deck deck = handler.DManager.LoadDeck(1, handler);
    //    handler.Close();
    //    Assert
    //    Assert.IsNotNull(deck);
    //    Assert.AreEqual(10, deck.CardDatas.Count);
    //    Assert.AreEqual(TagType.ITEM, deck.CardDatas[5].TType);



    //}

    //[Test]
    //public void CanSaveDeckToDB()
    //{
    //    handler = new DatabaseHandler();
    //    handler.CreateReferences();
    //    handler.ChangeDatabaseType(DatabaseType.Memory);

    //    handler.Open();
    //    CreateDBTable();
    //    Deck deck = new Deck();
    //    CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, "TagText", new int[2] { 3, 5 }, "Axe");

    //    for (int i = 0; i < 10; i++)
    //    {
    //        handler.CManager.SaveCardToDb(2, card, handler);
    //        deck.CardDatas.Add(card);
    //    }
    //    deck.ID = 2;
    //    deck.Name = "TestDeck";
    //    handler.DManager.SaveDeckToDb(deck, handler);

    //    Deck newDeck = handler.DManager.LoadDeck(2, handler);




    //    Assert.IsNotNull(newDeck);
    //    Assert.AreEqual(10, deck.CardDatas.Count);
    //    Assert.AreEqual("TestDeck", newDeck.Name);
    //    Assert.AreEqual(TagType.ITEM, deck.CardDatas[5].TType);

    //    handler.Close();
    //}

    //[Test]
    //public void CanDeleteDeck()
    //{
    //    handler = new DatabaseHandler();
    //    handler.CreateReferences();
    //    handler.ChangeDatabaseType(DatabaseType.Memory);

    //    handler.Open();
    //    CreateDBTable();

    //    Create deck
    //    Deck deck = new Deck();
    //    CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, "TagText", new int[2] { 3, 5 }, "Axe");

    //    for (int i = 0; i < 10; i++)
    //    {
    //        handler.CManager.SaveCardToDb(2, card, handler);
    //        deck.CardDatas.Add(card);
    //    }
    //    deck.ID = 2;
    //    deck.Name = "TestDeck";

    //    Save deck
    //    handler.DManager.SaveDeckToDb(deck, handler);

    //    Make sure it exists
    //    Deck tempDeck = handler.DManager.LoadDeck(2, handler);

    //    Delete deck
    //    handler.DManager.DeleteDeck(2, "'TestDeck'", handler);

    //    Try to load again
    //    Deck newDeck = handler.DManager.LoadDeck(2, handler);

    //    Assert that the deck existed in the first place, and that it is now deleted
    //    Assert.IsNotNull(tempDeck);
    //    Assert.AreEqual(10, tempDeck.CardDatas.Count);
    //    Assert.IsEmpty(newDeck.CardDatas);

    //    handler.Close();
    //}

}
