using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class AttachCardToButton : MonoBehaviour
{


    [SerializeField]
    public GameObject cardButtonPrefab;

    [SerializeField]
    public TextMeshProUGUI titleText;

    [SerializeField]
    public Card card;

    [SerializeField]
    public Image parent;


    [SerializeField]
    public Player player;

    public List<Card> cards;
    // Start is called before the first frame update
    void Start()
    {
        
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateCard()
    {
        for (int i = 0; i < player.hand.Count; i++)
        {
            
           var btn = Instantiate(cardButtonPrefab, parent.transform);
            btn.transform.SetParent(parent.transform, false);
           player.hand[i].gameObject.SetActive(false);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = player.hand[i].titleText.text;
                   
                      
        }
    }

    public void Activate()
    {
        
    //    player.hand.FindAll(x => x.titleText.text == card.titleText.text).FirstOrDefault().gameObject.SetActive(true);
       // card.gameObject.SetActive(false);
    }
}
