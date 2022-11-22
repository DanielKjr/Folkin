using Mono.Data.Sqlite;
using System.Collections.Generic;

public class CardMapper : ICardMapper
{
    public List<CardData> MapCardsFromReader(SqliteDataReader reader)
    {
        List<CardData> result = new List<CardData>();

        while (reader.Read())
        {
            var deckId = reader.GetInt32(1);
            var title = reader.GetString(2);
            var type = reader.GetString(3);
            var tag = reader.GetInt32(4);
            var tagText = reader.GetString(5);
            var description = reader.GetString(6);
            var icon = reader.GetString(7);
            var sprite = reader.GetString(8);

            //divide into strings using commas
            string[] iconSplit = icon.Split(',');
            int[] iconValues = new int[iconSplit.Length];


            for (int i = 0; i < iconSplit.Length; i++)
            {
                //Convert to integer
                iconValues[i] = int.Parse(iconSplit[i]);
                //if (char.IsDigit(iconSplit[i][i]))
                //{
                //    iconValues[i] = int.Parse(iconSplit[i]);
                //}

            }

            result.Add(new CardData(title, description, type, (TagType)tag, tagText, iconValues, sprite) { DeckID = deckId });
        }

        return result;
    }
}

