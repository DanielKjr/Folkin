using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeStage : MonoBehaviour
{
    public Card Card;
    string textInput;
    public DescriptionStage descriptionStage;
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
        textInput = s;
        Debug.Log(textInput);
    }
    public void SaveTextInputToCard()
    {
        if (textInput != null && textInput.Length > 0)
        {
            Card.typeText.text = textInput;
            descriptionStage.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {

        }

    }
}
