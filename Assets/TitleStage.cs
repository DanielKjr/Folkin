using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStage : MonoBehaviour
{
    public Card Card;
    string titleInput;
    public TypeStage typeStage;
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
        if (titleInput != null && titleInput.Length > 0)
        {
            Card.titleText.text = titleInput;
            typeStage.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            
        }
        
    }
}
