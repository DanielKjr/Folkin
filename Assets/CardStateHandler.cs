using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardStateHandler : MonoBehaviour
{
    public GameObject CreateCardbutton;
    public Card card;
    public TitleStage titleStage;
    public TypeStage typeStage;
    // Start is called before the first frame update
    void Start()
    {
        card.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateNewCard(string s)
    {
        card.gameObject.SetActive(true);
        titleStage.gameObject.SetActive(true);
        CreateCardbutton.SetActive(false);
       
    }
    
}
