using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardCreator : MonoBehaviour
{
    [SerializeField]
    public Player player;
    private CardCreator instance;
    public CardCreator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CardCreator();
            }
            return instance;
        }
    }
    public List<GameObject> allIcons = new List<GameObject>();
    public Card cardPrefab;
    public GameObject CardSilhuettePrefab;
    public GameObject CardIconPrefab;
    private bool spacereleased = true;
    private int[] iconTags;

    // Start is called before the first frame update
    void Start()
    {
        findAllIcons();
    }

    // Update is called once per frame
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spacereleased)
        {
            CreateCard("Axe", "Melee Weapon", CardType.BASECARD, "A sharp lump of metal attached to a wooden heft", "#Weapon | Medium \n Wooden | metallic", new int[3] {3, 4, 1} , "Sillhuettes/AxeSilhuette");
            //         string, string, enum, string, string, int[], string
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spacereleased = true;
        }
    }

    public void findAllIcons()
    {
        List<GameObject> icons = new List<GameObject>();
        Texture2D[] textures = Resources.LoadAll<Texture2D>("ICONS");
        for (int i = 0; i < textures.Length; i++)
        {
            GameObject gameObject = new GameObject("Icon");
            Image image = gameObject.AddComponent<Image>();
            image.sprite = Sprite.Create(textures[i], new Rect(0, 0, textures[i].width, textures[i].height), new Vector2(0.5f, 0.5f));
            icons.Add(gameObject);
            gameObject.transform.SetParent(transform, false);
        }
        allIcons = icons;
    }
    public GameObject[] Icons
    {
        get
        {
            
            List<GameObject> allIconTags = allIcons;
            GameObject[] _icons = new GameObject[iconTags.Length];
            int iconIndex = 0;
            foreach (int var in iconTags)
            {
                GameObject gameObject = new GameObject();
                gameObject = allIconTags[var];
                _icons[iconIndex] = gameObject;
                iconIndex++;
            }
            return _icons;
        }
    }

    void CreateCard(string title, string type, CardType ttype, string description, string tags, int[] iconValues, string silhuettepath)
    {
        //omskriv ikoner til at den fřrst finder dem frem hernede udfra et array af ints den fĺr, nĺr man kalder metoden
        var card = Instantiate(cardPrefab, new Vector2(5, 5), Quaternion.identity).GetComponent<Card>();
        card.SetCard(title, type, ttype, description, tags, iconValues, silhuettepath);
        var cardCanvas = card.GetComponentInChildren<Canvas>();

        CanvasSetup();
        


        iconTags = iconValues;
        Image cardPaper = cardCanvas.GetComponentInChildren<Image>();
        switch (ttype)
        {
            case CardType.SKILLCARD:
                cardPaper.color = Color.black;
                break;
            case CardType.BASECARD:
                cardPaper.color = Color.white;
                break;
        }

        CreateSilhuette();
        CreateIcons();
        PlayerHand
        player.AddToHand(card);

        void CreateSilhuette()
        {

            var cardSilhuette = Instantiate(CardSilhuettePrefab, Vector2.zero, Quaternion.identity);
            var cardSilhuetteImage = cardSilhuette.GetComponent<Image>();
            
            Texture2D cardSilhuetteTexture = (Texture2D)Resources.Load(silhuettepath);
            cardSilhuetteImage.sprite = Sprite.Create(cardSilhuetteTexture, new Rect(0, 0, cardSilhuetteTexture.width, cardSilhuetteTexture.height), new Vector2(0.5f, 0.5f));
            var cardSilhuetteCanvas = cardCanvas.GetComponentInChildren<Canvas>();
            cardSilhuette.transform.localScale = new Vector2(cardSilhuetteTexture.width/80, cardSilhuetteTexture.height/80);
            cardSilhuette.transform.SetParent(cardCanvas.transform, false);
            
            RectTransform rt = cardPaper.rectTransform;
            cardSilhuette.transform.localPosition = new Vector2(cardSilhuetteTexture.width, -cardSilhuetteTexture.height /2.5f);
            //instantiere Cardsilhuette prefabben. finder Texturen i Silhuette-folderen ved hjćlp af path'en dertil i string form. tager fat i cardsilhuette'ens
            //image og laver et sprite til den ud fra det den fandt i folderen. tager fat i canvas'et paa prefabben, og saetter den til at vaere parent
        }
        void CreateIcons()
        {
            var cardCanvas = card.GetComponentInChildren<Canvas>();
            int iconOffsetX = -137;
            int iconOffsetY = -225;
            int iconCount = 0;

            foreach (var icon in Icons)
            {
                var cardIcon = Instantiate(CardIconPrefab, Vector2.zero, Quaternion.identity);


                cardIcon.transform.SetParent(cardCanvas.transform, false);
                cardIcon.transform.localScale = new Vector2(0.8f, 0.8f);
                cardIcon.transform.localPosition = new Vector2(iconOffsetX, iconOffsetY);
                cardIcon.GetComponent<Image>().sprite = icon.GetComponent<Image>().sprite;

                iconOffsetX += 76;
                iconCount++;
                if (iconCount >= 2)
                {
                    iconOffsetX = -137;
                    iconOffsetY = -149;
                    iconCount = 0;
                }
                Destroy(icon);
            }
            


        }
        void CanvasSetup()
        {
            cardCanvas.renderMode = RenderMode.WorldSpace;
            cardCanvas.transform.position = new Vector2(0, 0);

            float s = 0.02f;
            cardCanvas.transform.localScale = new Vector3(s, s, s); //Why must i do this? Why not scale=0.02f ??
        }

    }
   
}
