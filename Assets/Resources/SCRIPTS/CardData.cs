public class CardData
{
    public int DeckID { get; set; }
    public string TitleText { get; set; }
    public string DescriptionText { get; set; }
    public string TypeText { get; set; }
    public string TagText { get; set; }
    public int[] IconValues { get; set; }
    public string IconString { get; set; }
    public string SpritePath { get; set; }
    public CardType CType { get; set; }
    public TagType TType { get; set; }

    public CardData(string titleText, string description, string cardType, TagType tagType, string tagText, int[] icons, string sprite)
    {
        TitleText = titleText;
        DescriptionText = description;
        TypeText = cardType;
        TType = tagType;
        TagText = tagText;
        IconValues = icons;
        SpritePath = sprite;

    }
}
