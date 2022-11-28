using System.Collections.Generic;

public interface ICardRepository
{
    /// <summary>
    /// Inserts a new CardData entry to the database
    /// </summary>
    /// <param name="deckId"></param>
    /// <param name="card"></param>
    void AddCard(int deckId, CardData card);

    /// <summary>
    /// Inserts a new Deck entry to the database, using a userID and a Deck reference
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="deck"></param>
    void AddDeck(int userId, Deck deck);

    /// <summary>
    /// Edits a card from the database by finding an entry with a matching deckID and name. 
    /// Deletes old entry and creates a new with the CardData parameter
    /// </summary>
    /// <param name="deckId"></param>
    /// <param name="cardName"></param>
    /// <param name="card"></param>
    void EditCard(int deckId, string cardName, CardData card);

    /// <summary>
    /// Deletes a card entry with the name provided.
    /// </summary>
    /// <param name="name"></param>
    void DeleteCard(string name);
    /// <summary>
    /// Deletes a card entry by using an existing references properties.
    /// </summary>
    /// <param name="card"></param>
    void DeleteCard(CardData card);

    /// <summary>
    /// Returns a CardData object that matches the name in the parameter.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    CardData FindCard(string name);
    /// <summary>
    /// Returns a CardData object that matches the name of the referenced CardData object.
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    CardData FindCard(CardData card);
    /// <summary>
    /// Returns a Deck object matching the name in parameter.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Deck FindDeck(string name);

    /// <summary>
    /// Returns a Deck object matching the referenced Decks name
    /// </summary>
    /// <param name="deck"></param>
    /// <returns></returns>
    Deck FindDeck(Deck deck);
    /// <summary>
    /// Returns a list of CardData objects with all the cards in the database.
    /// </summary>
    /// <returns></returns>
    List<CardData> GetAllCards();
    /// <summary>
    /// Returns a list of CardData objects that matches the deckID in the parameter
    /// </summary>
    /// <param name="deckId"></param>
    /// <returns></returns>
    List<CardData> GetAllCards(int deckId);
    /// <summary>
    /// Opens the connection to database.
    /// </summary>
    void Open();
    /// <summary>
    /// Closes the connection to database.
    /// </summary>
    void Close();

}

