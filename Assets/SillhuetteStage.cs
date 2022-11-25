using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SillhuetteStage : MonoBehaviour
{
    public Card Card;
    public GameObject CreateCardbutton;
    public List<Button> iconButtons = new List<Button>();
    public List<string> AllSillhuettePaths = new List<string>();
    private Canvas thisCanvas;
    public List<string> IconValues = new List<string>();
    public TextMeshProUGUI SillhuettePathText;
    public bool buttonHighlighted = false;
    public GameObject CardSillhuettePrefab;
    public string sillhuettePath;
    public string noSillhuettePath = "nopesry";
    public bool SillhuetteMade = false;

    private ICardRepository repository;
    private CardMapper mapper;
    private DatabaseProvider provider;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        thisCanvas = gameObject.GetComponentInChildren<Canvas>();
        FindAllSillhuettePaths();
        AddButtons();
        OrderButtons();
        AddAllButtonListeners();
        mapper = new CardMapper();
        provider = new DatabaseProvider("Data Source=CardDatabase.db; Version=3; New=False");
        repository = new CardRepository(provider, mapper);
    }

    // Update is called once per frame
    void Update()
    {
        SillhuettePathText.text = sillhuettePath;

    }
    public void FindAllSillhuettePaths()
    {
        Texture2D[] textures = Resources.LoadAll<Texture2D>("SILLHUETTES");
        int i = 0;
        List<string> iconPaths = new List<string>();
        foreach (Texture2D texture in textures)
        {
            string s = texture.name;
            string plusS = "SILLHUETTES/" + s;
            iconPaths.Add(plusS);
            i++;
        }
        AllSillhuettePaths = iconPaths;
        sillhuettePath = noSillhuettePath;
    }
    //"Assets/Resources/ICONS/Asset 706logo.png"
    public void AddButtons()
    {
        foreach (string sillhuettePath in AllSillhuettePaths)
        {
            GameObject ButtonObj = new GameObject("ButtonObj");
            Button button = ButtonObj.gameObject.AddComponent<Button>();
            Image buttonImage = ButtonObj.gameObject.AddComponent<Image>();
            Texture2D imgTexture = (Texture2D)Resources.Load(sillhuettePath);
            buttonImage.sprite = Sprite.Create(imgTexture, new Rect(0, 0, imgTexture.width, imgTexture.height), new Vector2(0.5f, 0.5f));
            ButtonObj.transform.SetParent(thisCanvas.transform, false);
            button.targetGraphic = buttonImage;
            iconButtons.Add(button);
        }
    }
    public void OrderButtons()
    {
        int horizontalButtons = 0;
        int buttonPositionX = -435;
        int buttonPositionY = -117;
        foreach (Button item in iconButtons)
        {
            item.transform.localPosition = new Vector2(buttonPositionX, buttonPositionY);
            buttonPositionX += 100;
            horizontalButtons++;
            if (horizontalButtons == 4)
            {
                buttonPositionX = -435;
                buttonPositionY -= 100;
                horizontalButtons = 0;
            }
        }
    }
    public void AddButtonListener(Button button, string filePath)
    {
        button.onClick.AddListener(() =>
        {

            if (sillhuettePath == "nopesry" || sillhuettePath != filePath)
            {
                //ColorBlock colorVar = button.colors;
                //colorVar.normalColor = Color.blue;
                //button.colors = colorVar;
                button.GetComponent<Image>().color = Color.green;
                ChangeSillhuettePath(filePath);
            }
            else
            {
                //ColorBlock colorVar = button.colors;
                //colorVar.normalColor = Color.red;
                //button.colors = colorVar;
                button.GetComponent<Image>().color = Color.white;
                EmptySillhuettePath(filePath);
            }
        }
        );
    }
    public void AddAllButtonListeners()
    {
        int index = 0;
        foreach (Button item in iconButtons)
        {
            AddButtonListener(item, AllSillhuettePaths[index]);
            index++;
        }
    }
    public void SaveInputToCard()
    {
        if (sillhuettePath != "nopesry")
        {

            Card.SpritePath = sillhuettePath;

            gameObject.SetActive(false);
            CreateCardbutton.SetActive(true);
            Card.gameObject.SetActive(false);
            SaveCardToDatabase(Card);
 
        }
        else
        {

        }

    }

    private  void SaveCardToDatabase(Card card)
    {
       
        CardData cardToSave = new CardData(card.titleText.text, card.descriptionText.text, card.typeText.text, card.TType, card.tagTexts, card.iconValues, card.SpritePath);
        repository.Open();
        repository.AddCard(1, cardToSave);


        repository.Close();
    }
    public void ChangeSillhuettePath(string filePath)
    {
        sillhuettePath = (filePath);
    }
    public void EmptySillhuettePath(string filePath)
    {
        sillhuettePath = noSillhuettePath;
    }

    public void CreateSilhuette()
    {
        var cardCanvas = Card.GetComponentInChildren<Canvas>();
        Image cardPaper = cardCanvas.GetComponentInChildren<Image>();
        if (SillhuetteMade)
        {
            foreach (Transform Sillhuette in cardPaper.transform)
            {
                if (Sillhuette.name == "CardSillhuette(Clone)")
                {
                    Destroy(Sillhuette.gameObject);
                }
            }
            SillhuetteMade = false;
        }
        if (sillhuettePath != "nopesry")
        {

            var cSillhuette = Instantiate(CardSillhuettePrefab, Vector2.zero, Quaternion.identity);           

            Texture2D cardSillhuetteTexture = (Texture2D)Resources.Load(sillhuettePath);

            var cardSillhuetteImage = cSillhuette.GetComponent<Image>();
            cardSillhuetteImage.sprite = Sprite.Create(cardSillhuetteTexture, new Rect(0, 0, cardSillhuetteTexture.width, cardSillhuetteTexture.height), new Vector2(0.5f, 0.5f));
            cardSillhuetteImage.SetNativeSize();
            cardSillhuetteImage.rectTransform.anchorMin = new Vector2(1, 0);
            cardSillhuetteImage.rectTransform.anchorMax = new Vector2(1, 0);
            cSillhuette.transform.SetParent(cardPaper.transform, false);
            //cSillhuette.transform.localScale = new Vector2(cardSillhuetteTexture.width / 80, cardSillhuetteTexture.height / 80);
            cSillhuette.transform.localPosition = new Vector2(cSillhuette.transform.localPosition.x - cardSillhuetteTexture.width / 2, cSillhuette.transform.localPosition.y + cardSillhuetteTexture.height / 2);
            //instantiere Cardsilhuette prefabben. finder Texturen i Silhuette-folderen ved hjælp af path'en dertil i string form. tager fat i cardsilhuette'ens
            //image og laver et sprite til den ud fra det den fandt i folderen. tager fat i canvas'et paa prefabben, og saetter den til at vaere parent
            SillhuetteMade = true;
        }
    }
}
