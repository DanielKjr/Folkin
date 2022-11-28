using System;

public class CardData
{
    public int DeckID { get;  set; }
    public string TitleText { get; private set; }
    public string DescriptionText { get; private set; }
    public string TypeText { get; private set; }
    public string[] TagText { get; private set; }
    public int[] IconValues { get; private set; }
    public string[] IconPath { get; private set; }
    public string SpritePath { get; private set; }
    public CardType CType { get; private set; }
    public TagType TType { get; private set; }

    public CardData(string titleText, string description, string cardType, TagType tagType, string[] tagText, string[] iconPath, string sprite)
    {
        TitleText = titleText;
        DescriptionText = description;
        TypeText = cardType;
        TType = tagType;
        TagText = tagText;
        IconPath = iconPath;
        SpritePath = sprite;
       

    }
}
