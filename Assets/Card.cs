using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI tagText;
    public Image[] cardIcons;
    public Image sillhuette;

    public Card(string TitleText, string DescriptionText, string TypeText, string TagText, Image[] cardIcons, Image sillhuette)
    {
        titleText.text = TitleText;
        descriptionText.text = DescriptionText;
        typeText.text = TypeText;
        tagText.text = TagText;
        this.cardIcons = cardIcons;
        this.sillhuette = sillhuette;
    }
    public void SetCard()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        int iconOffsetX = -50;
        int iconOffsetY = -100;
        int iconCount = 0;
        for (int i = 0; i < cardIcons.Length; i++)
        {
            //Vector2 imgVector = new Vector2(gameObject.transform.position.x - iconOffsetX, gameObject.transform.position.y - iconOffsetY);
            GameObject imgObject = new GameObject();
            Image img = imgObject.AddComponent<Image>();
            img.sprite = cardIcons[i].sprite;
            imgObject.transform.SetParent(gameObject.transform, false);
            imgObject.transform.localScale = new Vector2(iconOffsetX, iconOffsetY);

            Instantiate(imgObject);
            iconOffsetX -= 30;
            iconCount++;
            if (iconCount >= 3)
            {
                iconOffsetX = -50;
                iconOffsetY = -70;
                iconCount = 0;
            }
            //for at kortene skal være nær bunden, så tæl i stedet hvor mange billeder der er tilbage på listen og derefter hvis det er mindre end 3, så tegn de sidste i bunden
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
