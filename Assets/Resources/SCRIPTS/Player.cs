using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string playerName;
    public int HP;
    public List<Card> hand = new List<Card>();

    /// <summary>
    /// Add card to player hand
    /// </summary>
    /// <param name="card"></param>

    public void AddToHand(Card card)
    {
        hand.Add(card);
        AutoShiftCards();
    }
    /// <summary>
    /// Remove card from player hand
    /// </summary>
    /// <param name="card"></param>
    public void RemoveFromHand(Card card)
    {
        hand.Remove(card);
    }
    /// <summary>
    /// Scale cards in players hand
    /// </summary>
    /// <param name="card"></param>
    /// <param name="newScale"></param>
    /// <param name="ScaleAllCards"></param>
    public void CardScale(Card card, float newScale, bool ScaleAllCards)
    {
        if (!ScaleAllCards)
        {
            var cardCanvas = card.GetComponentInChildren<Canvas>();
            cardCanvas.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
        else
        {
            foreach (var item in hand)
            {
                var cardCanvas = item.GetComponentInChildren<Canvas>();
                cardCanvas.transform.localScale = new Vector3(newScale, newScale, newScale);
            }
        }
    }
    /// <summary>
    /// Move cards in players hand
    /// </summary>
    /// <param name="card"></param>
    /// <param name="newPosition"></param>
    public void CardMove(Card card, Vector2 newPosition)
    {
        var cardCanvas = card.GetComponentInChildren<Canvas>();
        cardCanvas.transform.position = newPosition;
    }
    /// <summary>
    /// Auto called from AddToHand, scales and moves cards to fit screen.
    /// </summary>
    private void AutoShiftCards()
    {
        float x = 0;
        float standardScale = 0.01f;
        float handCount = hand.Count / 5;
        float mathScaled = standardScale - handCount / 1000;
        float cardWidth = 354 * mathScaled;
        float y = 0;
        float offset = cardWidth;
        for (int i = 0; i < (int)hand.Count; i++)
        {
            x++;
            if (x * cardWidth > 18)
            {
                x = 1;
                y -= 2;
            }
            CardMove(hand[i], new Vector2((x * offset) - 10, y));
        }
        CardScale(null, mathScaled, true);
    }
}
