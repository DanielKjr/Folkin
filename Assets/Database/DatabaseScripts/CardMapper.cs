using Mono.Data.Sqlite;
using System.Collections.Generic;

public class CardMapper : ICardMapper
{
    ///<inheritdoc />
    public List<CardData> MapCardsFromReader(SqliteDataReader reader)
    {
        List<CardData> result = new List<CardData>();

        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var deckId = reader.GetInt32(1);
            var title = reader.GetString(2);
            var type = reader.GetString(3);
            var tag = reader.GetInt32(4);
            var tagText = reader.GetString(5);
            var description = reader.GetString(6);
            var icon = reader.GetString(7);
            var sprite = reader.GetString(8);

    
            string[] iconPathSplit = icon.Split(',');
            string[] iconPaths = new string[iconPathSplit.Length];
            string[] tagTextSplit = tagText.Split(",");
            string[] tagTexts = new string[tagTextSplit.Length];


            for (int i = 0; i < iconPathSplit.Length; i++)
            {
                iconPaths[i] = iconPathSplit[i];
            }

            for (int i = 0; i < tagTextSplit.Length; i++)
            {
                tagTexts[i] = tagTextSplit[i];
            }

            result.Add(new CardData(title, description, type, (TagType)tag, tagTexts, iconPaths, sprite) { DeckID = deckId, ID = id });
        }

        return result;
    }
}

