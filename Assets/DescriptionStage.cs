using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionStage : MonoBehaviour
{
    public Card Card;
    string textInput;
    public TagStage tagStage;
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
            Card.descriptionText.text = textInput;
            gameObject.SetActive(false);
            tagStage.gameObject.SetActive(true);
        }
        else
        {

        }

    }
}
