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
        var provider = new DatabaseProvider("Data Source=CardDatabase.db; Version=3; New=false");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        // CardData card = new CardData("Axe", "Damage", "Action", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        //  repository.AddCard(2, card);

        var result = repository.GetAllCards();

        Assert.IsNotNull(result);
        Assert.AreEqual("ICONS/ASSET 706logo", result[0].IconPath[0]);
        Assert.AreEqual("ICONS/ASSET 702logo", result[1].IconPath[1]);
        repository.Close();
    }

    [Test]
    public void CanAddCard()
    {
        //Arrange
        ICardRepository repository;
        var mapper = new CardMapper();
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");

        //Act
        repository.AddCard(1, card);
        var result = repository.FindCard(card);

        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("TitleText", result.TitleText);
        Assert.AreEqual("DescriptionText", result.DescriptionText);
        Assert.AreEqual("CardType", result.TypeText);
        Assert.AreEqual(TagType.ITEM, result.TType);
        Assert.AreEqual("TagText", result.TagText[0]);
        Assert.AreEqual("ICONS/ASSET 706logo", result.IconPath[0]);
        Assert.AreEqual("ICONS/ASSET 702logo", result.IconPath[1]);
        Assert.AreEqual("Axe", result.SpritePath);
        repository.Close();
    }

    [Test]
    public void CanAddCardWithOneIconOrTag()
    {
        //Arrange
        ICardRepository repository;
        var mapper = new CardMapper();
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[1] { "TagText" }, new string[1] { "ICONS/ASSET 706logo" }, "Axe");
        //Act
        repository.AddCard(1, card);
        var result = repository.FindCard(card);

        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("TitleText", result.TitleText);
        Assert.AreEqual("DescriptionText", result.DescriptionText);
        Assert.AreEqual("CardType", result.TypeText);
        Assert.AreEqual(TagType.ITEM, result.TType);
        Assert.AreEqual("TagText", result.TagText[0]);
        Assert.AreEqual("ICONS/ASSET 706logo", result.IconPath[0]);
        //  Assert.AreEqual("5", result.IconPath[1]);
        Assert.AreEqual("Axe", result.SpritePath);
        repository.Close();
    }

    [Test]
    public void CanAddCardWithThreeIconOrTag()
    {
        //Arrange
        ICardRepository repository;
        var mapper = new CardMapper();
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[3] { "TagText", "Text", "MoreText" },
            new string[3] { "ICONS/ASSET 706logo", "ICONS/ASSET 701logo", "ICONS/ASSET 705logo" }, "Axe");
        //Act
        repository.AddCard(1, card);
        var result = repository.FindCard(card);

        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("TitleText", result.TitleText);
        Assert.AreEqual("DescriptionText", result.DescriptionText);
        Assert.AreEqual("CardType", result.TypeText);
        Assert.AreEqual(TagType.ITEM, result.TType);
        Assert.AreEqual("TagText", result.TagText[0]);
        Assert.AreEqual("Text", result.TagText[1]);
        Assert.AreEqual("MoreText", result.TagText[2]);
        Assert.AreEqual("ICONS/ASSET 706logo", result.IconPath[0]);
        Assert.AreEqual("ICONS/ASSET 701logo", result.IconPath[1]);
        Assert.AreEqual("ICONS/ASSET 705logo", result.IconPath[2]);
        Assert.AreEqual("Axe", result.SpritePath);
        repository.Close();
    }

    [Test]
    public void CanAddDeck()
    {
        ICardRepository repository;

        //Arrange
        var mapper = new CardMapper();
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        Deck deck = new Deck();
        deck.Name = "Dummy Deck";
        deck.ID = 2;
        CardData[] cards = new CardData[3];
        cards[0] = new CardData("Axe", "Damage", "Action", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        cards[1] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        cards[2] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");

        for (int i = 0; i < cards.Length; i++)
        {
            deck.CardDatas.Add(cards[i]);
        }

        //Act
        repository.AddDeck(deck.ID, deck);

        var result = repository.FindDeck(deck);

        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.ID);
        Assert.AreEqual(3, result.CardDatas.Count);
        Assert.AreEqual(TagType.ITEM, result.CardDatas[2].TType);
        Assert.AreEqual("Dummy Deck", result.Name);

        repository.Close();
    }

    [Test]
    public void CanFindCardByReference()
    {
        ICardRepository repository;

        //Arrange
        var mapper = new CardMapper();
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        //Act
        CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        repository.AddCard(1, card);
        var result = repository.FindCard(card);


        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("TitleText", result.TitleText);
        Assert.AreEqual(TagType.ITEM, result.TType);
        Assert.AreEqual(card.IconPath, result.IconPath);
        repository.Close();
    }

    [Test]
    public void CanFindCardByName()
    {
        ICardRepository repository;

        //Arrange
        var mapper = new CardMapper();
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        //Act
        CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        repository.AddCard(1, card);

        var result = repository.FindCard("TitleText");


        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("TitleText", result.TitleText);
        Assert.AreEqual(TagType.ITEM, result.TType);
        Assert.AreEqual(1, result.DeckID);
        repository.Close();
    }

    [Test]
    public void CanGetAllCards()
    {
        ICardRepository repository;

        var mapper = new CardMapper();
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData[] cards = new CardData[3];
        cards[0] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        cards[1] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Sword");
        cards[2] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Potion");

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
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData[] cards = new CardData[3];
        cards[0] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        cards[1] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Sword");
        cards[2] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Potion");

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
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();
        Deck deck = new Deck();
        deck.ID = 2;
        deck.Name = "TestDeck";
        CardData[] cards = new CardData[3];
        cards[0] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        cards[1] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Sword");
        cards[2] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Potion");


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
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();
        Deck deck = new Deck();
        deck.ID = 1;
        deck.Name = "TestDeck";
        CardData[] cards = new CardData[3];
        cards[0] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        cards[1] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Sword");
        cards[2] = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Potion");


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
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
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
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("TitleText", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");

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
        var provider = new DatabaseProvider("Data Source=:memory:; Version=3; New=True");
        repository = new CardRepository(provider, mapper);
        repository.Open();

        CardData card = new CardData("Axe", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        repository.AddCard(1, card);

        CardData newcard = new CardData("Sword", "DescriptionText", "CardType", TagType.ITEM, new string[2] { "TagText", "Text" }, new string[2] { "ICONS/ASSET 706logo", "ICONS/ASSET 702logo" }, "Axe");
        repository.EditCard(1, "Axe", newcard);

        var result = repository.FindCard(newcard);
        var oldResult = repository.FindCard(card);

        Assert.IsNotNull(result);
        Assert.IsNull(oldResult);
        Assert.AreEqual("Sword", result.TitleText);

        repository.Close();
    }

}
