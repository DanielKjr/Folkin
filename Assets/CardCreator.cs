using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardCreator : MonoBehaviour
{
    public Image[] allIcons
    {
        get
        {
            Texture2D[] textures = Resources.LoadAll<Texture2D>("Assets/ICONS");
            for (int i = 0; i < textures.Length; i++)
            {
                Image image = gameObject.AddComponent<Image>();
                image.sprite = Sprite.Create(textures[i], new Rect(0, 0, textures[i].width, textures[i].height), new Vector2(0.5f, 0.5f));
                allIcons[i] = image;
            }
            return allIcons;
        }
    }
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

    public Image axesillhuette;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public Image[] Icons
    //{
    //    get 
    //    {
    //        int i = 0;
    //        foreach (blabla in blablalist)
    //        {
    //            Image var = blablalist.Find(x = x.tag = blabla.tag)
    //            Icons[i] = blabla;
    //            i++;
    //        }
    //    }

    //}
        
  
    void CreateCard(string title, string type, string description, string tags, Image[] icons, Image sillhuette)
    {
        List<Image> images = new List<Image>();
        images.Add(axesillhuette);
        Card testcard = new Card("axe", "this is an axe", "equipment", "weapon \n wooden", icons, sillhuette);
        Instantiate(testcard);
    }
}
