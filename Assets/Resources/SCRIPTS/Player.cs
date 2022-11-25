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
        AutoShiftCards();
    }
    public void RemoveFromHand(Card card)
    {
        hand.Remove(card);
    }
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
    public void CardMove(Card card, Vector2 newPosition)
    {
        var cardCanvas = card.GetComponentInChildren<Canvas>();
        cardCanvas.transform.position = newPosition;
    }
    private void AutoShiftCards()
    {
        int offset = 3; //CHANGE OFFSET HERE <-----
        float count = hand.Count;
        if (count > 1)
        {
            int x = 0;
            float y = 0;
            for (int i = 0; i < (int)count; i++)
            {
                x++;
                if (x > 6)
                {
                    x = 1;
                    y -= 2;
                }
                //CardMove(hand[i], new Vector2((i * offset) - count + 1, 0));
                Mover(hand[i], x, offset, (int)y);
            }
            float scale = 0.01f;
            float posY = -1f * y;
            float math = posY / 1000;
            float mather = scale - math;
            CardScale(null, mather, true);
        }
    }
    private void Mover(Card card, int pos, int offset, int y)
    {
        CardMove(card, new Vector2((pos * offset) - 10, y));
    }
}
