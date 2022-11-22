using System.Collections.Generic;

public interface ICardRepository
{
    void AddCard(int deckId, CardData card);
    void AddDeck(int userId, Deck deck);
    void EditCard(int deckId, string cardName, CardData card);
    void DeleteCard(string name);
    void DeleteCard(CardData card);

    CardData FindCard(string name);
    CardData FindCard(CardData card);
    Deck FindDeck(string name);
    Deck FindDeck(Deck deck);
    List<CardData> GetAllCards();
    List<CardData> GetAllCards(int deckId);
    void Open();
    void Close();

}

