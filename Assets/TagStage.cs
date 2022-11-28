using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TagStage : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField inputField;
    public Card Card;
    string textInput;
    public IconsStage iconsStage;
    public List<string> tagList;
    public int tagCount;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        tagCount = 0;
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
        if (textInput != null && tagList.Count > 0)
        {
            tagList.Add(textInput);
            gameObject.SetActive(false);
            iconsStage.gameObject.SetActive(true);
            Card.tagTexts = tagList.ToArray();
        }
        else
        {

        }

    }
    public void AddAnotherTag()
    {
        if(textInput != null && textInput.Length > 0)
        {
            tagList.Add(textInput);
            Card.tagText.text = StringFromList(tagList);
            inputField.gameObject.transform.localPosition = new Vector2(inputField.gameObject.transform.localPosition.x, inputField.gameObject.transform.localPosition.y - 25);
        }
     
        
    }
    string StringFromList(List<string> strings)
    {
        var result = new StringBuilder();
        foreach (string s in strings)
        {
            result.Append($"{s}\n");
        }
        return result.ToString();
    }
}