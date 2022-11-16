using System.Collections.Generic;

public class Deck
{
    private readonly int deckSize = 30;
    public List<Card> cards = new List<Card>();
    public int ID { get; set; }
    public string Name { get; set; }


    public void AddCard(Card card)
    {
        if (cards.Count < deckSize)
        {
            cards.Add(card);
        }
        //Add error if not added?
    }
    public void RemoveCard(Card card)
    {
        cards.Remove(card);
    }
}
