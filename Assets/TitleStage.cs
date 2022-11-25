using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStage : MonoBehaviour
{
    public Card Card;
    string titleInput;
    public TypeStage typeStage;
    public bool CardTypeChosen = false;
    public CardType cardType;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string s)
    {
        titleInput = s;
        Debug.Log(titleInput);
    }
    public void SaveTitleToCard()
    {
        if (titleInput != null && titleInput.Length > 0 && CardTypeChosen)
        {
            Card.titleText.text = titleInput;
            Card.CType = cardType;
            typeStage.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            
        }
        
    }
    public void ChangeCTypeToBlack()
    {
        cardType = CardType.SKILLCARD;
        CardTypeChosen = true;
    }
    public void ChangeCTypeToWhite()
    {
        cardType = CardType.BASECARD;
        CardTypeChosen = true;
    }
}
