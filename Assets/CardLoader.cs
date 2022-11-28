using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLoader : MonoBehaviour
{

    private ICardRepository repository;
    private CardMapper mapper;
    private DatabaseProvider provider;

    [SerializeField]
    public CardCreator creator;

    private bool spacereleased = true;


    // Start is called before the first frame update
    void Start()
    {
        mapper = new CardMapper();
        provider = new DatabaseProvider("Data Source=CardDatabase.db; Version=3; New=False");
        repository = new CardRepository(provider, mapper);
    }

    public void LoadAllCards()
    {
        repository.Open();
        List<CardData> cards = new List<CardData>();
        cards = repository.GetAllCards();

        foreach (CardData card in cards)
        {
            creator.CreateCard(card.TitleText, card.TypeText, card.CType, card.DescriptionText, card.TagText, card.IconPath, card.SpritePath);
        }

        repository.Close();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spacereleased)
        {
            LoadAllCards();
            //         string, string, enum, string, string, int[], string
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spacereleased = true;
        }
    }
}
