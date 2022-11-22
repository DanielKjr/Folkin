using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum CardType { BASECARD, SKILLCARD }
public enum TagType { SKILL, ITEM }
public class Card : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI tagText;
    public int[] iconValues;
    public Image sillhuette;
    public string SpritePath { get; set; }
    public CardType CType { get; set; }
    public TagType TType { get; set; }
    public Card(string titleText, string description, string cardType, TagType tagType, string tagText, int[] icons, string sprite)
    {
        
    }

    public void SetCard(string TitleText, string TypeText, CardType cardType, string DescriptionText, string TagText, int[] iconValues, string silhuettePath)
    {
        titleText.text = TitleText;
        descriptionText.text = DescriptionText;
        typeText.text = TypeText;
        tagText.text = TagText;
        this.iconValues = iconValues;
        //husk at sætte disse inde i prefabben
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
