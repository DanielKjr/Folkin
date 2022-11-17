using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum TType { black, white}
public class Card : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI tagText;
    public GameObject[] cardIcons;
    public Image sillhuette;

    public void SetCard(string TitleText, string TypeText, TType type, string DescriptionText, string TagText, GameObject[] cardIcons, string silhuettePath)
    {
        titleText.text = TitleText;
        descriptionText.text = DescriptionText;
        typeText.text = TypeText;
        tagText.text = TagText;
        this.cardIcons = cardIcons;
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
