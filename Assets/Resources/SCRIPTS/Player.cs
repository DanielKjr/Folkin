using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string playerName;
    public int HP;
    public List<Card> hand = new List<Card>();
    public void AddToHand(Card card)
    {
        hand.Add(card);
        card.transform.position = new Vector2(0,0);
        var cardCanvas = card.GetComponentInChildren<Canvas>();
        Image cardPaper = cardCanvas.GetComponentInChildren<Image>();
        cardPaper.transform.position = new Vector2(172, 0);
    }
    public void RemoveFromHand(Card card)
    {
        hand.Remove(card);
    }
}
