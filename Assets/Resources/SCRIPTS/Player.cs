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
    }
    public void RemoveFromHand(Card card)
    {
        hand.Remove(card);
    }
    public void CardScale(Card card, float newScale)
    {
        var cardCanvas = card.GetComponentInChildren<Canvas>();
        cardCanvas.transform.localScale = new Vector3(newScale,newScale,newScale);
    }
    public void CardMove(Card card, Vector2 newPosition)
    {
        var cardCanvas = card.GetComponentInChildren<Canvas>();
        cardCanvas.transform.position = newPosition;
    }
}