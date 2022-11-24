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
    public List<string> allIconPaths = new List<string>();
    public Card cardPrefab;
    public GameObject CardSillhuettePrefab;
    public GameObject CardIconPrefab;
    private bool spacereleased = true;
    private string[] iconValues;
    // Start is called before the first frame update
    void Start()
    {
        FindAllIconPaths();
    }

    // Update is called once per frame
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spacereleased)
        {
            CreateCard("Axe", "Melee Weapon", CardType.BASECARD, "A sharp lump of metal attached to a wooden heft", new string[2] { "#Weapon | Medium", "Wooden | metallic" }, new string[1] { "ICONS/Asset 706logo" } , "SILLHUETTES/Asset 684logo");
            //         string, string, enum, string, string, int[], string
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spacereleased = true;
        }
    }

    public void FindAllIconPaths()
    {
        Texture2D[] textures = Resources.LoadAll<Texture2D>("ICONS");
        int i = 0;
        List<string> iconPaths = new List<string>();
        foreach (Texture2D texture in textures)
        {
            string s = AssetDatabase.GetAssetPath(textures[i]);
            string subS = s.Substring(17);
            string subSubS = subS.Substring(0, subS.Length - 4);
            iconPaths.Add(subSubS);
            i++;
        }
        allIconPaths = iconPaths;
    }

    public GameObject[] Icons
    {
        get
        {
            GameObject[] _icons = new GameObject[iconValues.Length];
            int iconIndex = 0;
            foreach (string var in iconValues)
            {
                GameObject gameObject = new GameObject();
                Texture2D imgTexture = (Texture2D)Resources.Load(var);
                Image iconIMG = gameObject.gameObject.AddComponent<Image>();
                iconIMG.sprite = Sprite.Create(imgTexture, new Rect(0, 0, imgTexture.width, imgTexture.height), new Vector2(0.5f, 0.5f));

                _icons[iconIndex] = gameObject;
                iconIndex++;
            }
            return _icons;
        }
    }

    void CreateCard(string title, string type, CardType ttype, string description, string[] tags, string[] iconPaths, string sillhuettePath)
    {
        //omskriv ikoner til at den fřrst finder dem frem hernede udfra et array af ints den fĺr, nĺr man kalder metoden
        var card = Instantiate(cardPrefab, new Vector2(5, 5), Quaternion.identity).GetComponent<Card>();
        card.SetCard(title, type, ttype, description, tags, iconPaths, sillhuettePath);
        var cardCanvas = card.GetComponentInChildren<Canvas>();


        CanvasSetup();


        iconTags = iconValues;

        Image cardPaper = cardCanvas.GetComponentInChildren<Image>();
        iconValues = iconPaths;
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

        card.gameObject.SetActive(true);

        //PlayerHand
        player.AddToHand(card);
        card.gameObject.SetActive(true);

        void CreateSilhuette()
        {
            //instantiere Cardsilhuette prefabben. finder Texturen i Silhuette-folderen ved hj�lp af path'en dertil i string form. tager fat i cardsilhuette'ens

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
 
            Image cardPaper = cardCanvas.GetComponentInChildren<Image>();
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
        }

        //void CreateIcons()
        //{
        //    var cardCanvas = card.GetComponentInChildren<Canvas>();
        //    int iconOffsetX = -137;
        //    int iconOffsetY = -225;
        //    int iconCount = 0;

        //    foreach (var icon in Icons)
        //    {
        //        var cardIcon = Instantiate(CardIconPrefab, Vector2.zero, Quaternion.identity);


        //        cardIcon.transform.SetParent(cardCanvas.transform, false);
        //        cardIcon.transform.localScale = new Vector2(0.8f, 0.8f);
        //        cardIcon.transform.localPosition = new Vector2(iconOffsetX, iconOffsetY);
        //        cardIcon.GetComponent<Image>().sprite = icon.GetComponent<Image>().sprite;

        //        iconOffsetX += 76;
        //        iconCount++;
        //        if (iconCount >= 2)
        //        {
        //            iconOffsetX = -137;
        //            iconOffsetY = -149;
        //            iconCount = 0;
        //        }
        //        Destroy(icon);
        //    }



        //}
        void CreateIcons()
        {
            //int paperLeft = (int)cardPaper.transform.position.x - (); -2 / 354, -2 / 531
            //int paperBottom = (int)cardPaper.transform.position.y;
            int iconOffsetX = -141;
            int iconOffsetY = -230;
            int iconCount = 0;

            foreach (var icon in Icons)
            {
                var cardIcon = Instantiate(CardIconPrefab, Vector2.zero, Quaternion.identity);


                cardIcon.transform.SetParent(cardPaper.transform, false);
                cardIcon.transform.localScale = new Vector2(0.7f, 0.7f);
                cardIcon.transform.localPosition = new Vector2(iconOffsetX, iconOffsetY);
                cardIcon.GetComponent<Image>().sprite = icon.GetComponent<Image>().sprite;

                iconOffsetX += 76;
                iconCount++;
                if (iconCount >= 2)
                {
                    iconOffsetX = -142;
                    iconOffsetY += 76;
                    iconCount = 0;
                }
                //Destroy(icon);
            }
        }

        void CanvasSetup()
        {
            cardCanvas.renderMode = RenderMode.WorldSpace;
            cardCanvas.transform.position = new Vector2(0, 0);

            float s = 0.01f;
            cardCanvas.transform.localScale = new Vector3(s, s, s); //Why must i do this? Why not scale=0.02f ??
        }


    }
}
