using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public CardLoader loader;

    [SerializeField]
    public TMP_InputField inputField;
    [SerializeField]
    public UnityEngine.UI.Button submitButton;

    private ICardRepository repository;
    private CardMapper mapper;
    private DatabaseProvider provider;
    private bool enterReleased = true;
    // Start is called before the first frame update
    void Start()
    {
        mapper = new CardMapper();
        provider = new DatabaseProvider("Data Source=CardDatabase.db; Version=3; New=False");
        repository = new CardRepository(provider, mapper);

        inputField.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Submit()
    {
        if (inputField.gameObject.activeInHierarchy)
        {


            try
            {
                if (inputField.text == string.Empty)
                {
                    loader.LoadAllCards();
                    inputField.gameObject.SetActive(false);
                    submitButton.gameObject.SetActive(false);

                }
                else
                {
                    int id = Convert.ToInt32(inputField.text);
                    loader.LoadAllCards(id);
                    inputField.gameObject.SetActive(false);
                    submitButton.gameObject.SetActive(false);
                }

            }
            catch (FormatException e)
            {

                inputField.text = e.Message;
            }







        }
      
    }

    public void ClearInput()
    {
        inputField.text = string.Empty;
    }

    public void LoadAllCards()
    {
        loader.LoadAllCards();
    }

    public void LoadAllCardsWithID()
    {
        inputField.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(true);

    }

    public void ShowAllDecks()
    {
        repository.Open();

        Deck deck = new Deck();
        deck.ID = 1;
        deck.Name = "Standard";
        deck = repository.FindDeck(deck);


        repository.Close();
    }

    public void FindDeck()
    {

    }
}
