using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public enum CardType { BASECARD, SKILLCARD }
public enum TagType { SKILL, ITEM }
public class Card : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI tagText;
    public string[] iconValues;
    public Image sillhuette;
    public string[] tagTexts;
    public string SpritePath { get; set; }
    public CardType CType { get; set; }
    public TagType TType { get; set; }
    public Card(string titleText, string description, string cardType, TagType tagType, string[] tagText, string[] iconPaths, string sprite)
    {
        
    }

    public void SetCard(string TitleText, string TypeText, CardType cardType, string DescriptionText, string[] TagTexts, string[] iconPaths, string silhuettePath)
    {
        titleText.text = TitleText;
        descriptionText.text = DescriptionText;
        typeText.text = TypeText;
        tagText.text = StringFromList(TagTexts);
        this.iconValues = iconPaths;
        //husk at sætte disse inde i prefabben
    }
    public string StringFromList(string[] strings)
    {
        var result = new StringBuilder();
        foreach (string s in strings)
        {
            result.Append($"{s}\n");
        }
        return result.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
            //for at kortene skal være nær bunden, så tæl i stedet hvor mange billeder der er tilbage på listen og derefter hvis det er mindre end 3, så tegn de sidste i bunden
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
