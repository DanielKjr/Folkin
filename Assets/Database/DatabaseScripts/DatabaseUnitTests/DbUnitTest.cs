using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DbUnitTest
{





   
    [Test]
    public void CanAddAndReadFromDbFile()
    {

        ICardRepository repository;

        //Arrange
        var mapper = new CardMapper();
        var provider = new SQLiteDatabaseProvider("Data Source=CardDatabase.db; Version=3; New=false");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("Axe", "Chop chop", "Skill", TagType.SKILL, "Axe", new int[2] { 1, 2 }, "Axe");
        repository.AddCard(2, card);
        var result = repository.GetAllCards();

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result[0].IconValues[0]);
        Assert.AreEqual(1, result[1].IconValues[0]);
        repository.Close();
    }

    [Test]
    public void CanAddCard()
    {
        ICardRepository repository;

        //Arrange
        var mapper = new CardMapper();
        var provider = new SQLiteDatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, "TagText", new int[2] { 3, 5 }, "Axe");
        repository.AddCard(1, card);

        var result = repository.FindCard(card);

        Assert.IsNotNull(result);
        Assert.AreEqual(TagType.ITEM, result.TType);

        repository.Close();
    }

    [Test]
    public void CanAddDeck()
    {
        ICardRepository repository;

        //Arrange
        var mapper = new CardMapper();
        var provider = new SQLiteDatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        Deck deck = new Deck();
        deck.Name = "Dummy Deck";
        deck.ID = 2;
        CardData[] cards = new CardData[3];
        cards[0] = new CardData("Axe", "Damage", "Action", TagType.ITEM, "ChopChop", new int[2] { 3, 5 }, "Axe");
        cards[1] = new CardData("Sword", "Damage", "CardType", TagType.ITEM, "Poke", new int[2] { 3, 5 }, "Sword");
        cards[2] = new CardData("Potion", "Heal", "CardType", TagType.ITEM, "Chug", new int[2] { 3, 5 }, "Potion");

        for (int i = 0; i < cards.Length; i++)
        {
            deck.CardDatas.Add(cards[i]);
        }

        repository.AddDeck(deck.ID, deck);

        var result = repository.FindDeck(deck);
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.ID);
        Assert.AreEqual(3, result.CardDatas.Count);

        repository.Close();
    }

    [Test]
    public void CanFindCardByReference()
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
    public void CanFindCardByName()
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

        var result = repository.FindCard("TitleText");


        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("TitleText", result.TitleText);
        Assert.AreEqual(TagType.ITEM, result.TType);
        repository.Close();
    }

    [Test]
    public void CanGetAllCards()
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
    public void CanGetAllCardsByDeckID()
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

        var result = repository.GetAllCards(1);

        Assert.IsNotNull(result);
        Assert.AreEqual("Axe", result[0].SpritePath);
        Assert.AreEqual("Sword", result[1].SpritePath);
        Assert.AreEqual("Potion", result[2].SpritePath);

        repository.Close();
    }

    [Test]
    public void CanFindDeckByReference()
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

        repository.AddDeck(deck.ID, deck);

        var result = repository.FindDeck(deck);
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.CardDatas.Count);
        Assert.AreEqual(2, result.CardDatas[1].DeckID);
        repository.Close();
    }


    [Test]
    public void CanFindDeckByName()
    {
        ICardRepository repository;

        var mapper = new CardMapper();
        var provider = new SQLiteDatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();
        Deck deck = new Deck();
        deck.ID = 1;
        deck.Name = "TestDeck";
        CardData[] cards = new CardData[3];
        cards[0] = new CardData("Axe", "Damage", "Action", TagType.ITEM, "ChopChop", new int[2] { 3, 5 }, "Axe");
        cards[1] = new CardData("Sword", "Damage", "CardType", TagType.ITEM, "Poke", new int[2] { 3, 5 }, "Sword");
        cards[2] = new CardData("Potion", "Heal", "CardType", TagType.ITEM, "Chug", new int[2] { 3, 5 }, "Potion");

        for (int i = 0; i < cards.Length; i++)
        {
            deck.CardDatas.Add(cards[i]);

        }

        repository.AddDeck(deck.ID, deck);

        var result = repository.FindDeck(deck.Name);
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.CardDatas.Count);

        repository.Close();
    }


    [Test]
    public void CanDeleteCardByReference()
    {
        ICardRepository repository;

        var mapper = new CardMapper();
        var provider = new SQLiteDatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("Axe", "Chopchop", "Skill", TagType.SKILL, "You can swing your axe", new int[2] { 3, 2 }, "Axe");
        repository.AddCard(1, card);

        repository.DeleteCard(card);
        var result = repository.FindCard(card);

        Assert.IsNull(result);
        Assert.IsNotNull(card);
        repository.Close();
    }

    [Test]
    public void CanDeleteCardByName()
    {
        ICardRepository repository;

        var mapper = new CardMapper();
        var provider = new SQLiteDatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("Axe", "Chopchop", "Skill", TagType.SKILL, "You can swing your axe", new int[2] { 3, 2 }, "Axe");
        repository.AddCard(1, card);

        repository.DeleteCard("Axe");
        var result = repository.FindCard(card);

        Assert.IsNull(result);
        Assert.IsNotNull(card);
        repository.Close();
    }


    [Test]
    public void CanEditCard()
    {
        ICardRepository repository;

        var mapper = new CardMapper();
        var provider = new SQLiteDatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("Axe", "Chopchop", "Skill", TagType.SKILL, "You can swing your axe", new int[2] { 3, 2 }, "Axe");
        repository.AddCard(1, card);

        CardData newcard = new CardData("Sword", "Schwing", "Skill", TagType.SKILL, "Pokey pokey", new int[2] { 1, 2 }, "Sword");
        repository.EditCard(1, "Axe", newcard);

        var result = repository.FindCard(newcard);
        var oldResult = repository.FindCard(card);

        Assert.IsNotNull(result);
        Assert.IsNull(oldResult);
        Assert.AreEqual("Sword", result.TitleText);

        repository.Close();
    }

}
