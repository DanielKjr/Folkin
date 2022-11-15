using Mono.Data.Sqlite;
using UnityEditor.MemoryProfiler;

public class DeckManager : DatabaseHandler
{
    private static DeckManager instance;
    private Deck currentDeck;

    public static DeckManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DeckManager();
            }
            return instance;
        }
    }



    /// <summary>
    /// Loads the deck from database with the corresponding ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public Deck LoadDeck(int ID)
    {
        Deck deck = new Deck();
        Open();

        var cmd = new SqliteCommand($"SELECT Title, Type, Tag, TagText, Description, Icon, Sprite FROM 'Card' WHERE DeckID={ID}", (SqliteConnection)connection);
        var dataRead = cmd.ExecuteReader();

        while (dataRead.Read())
        {
            string title = dataRead.GetString(0);
            string type = dataRead.GetString(1);
            int tag = dataRead.GetInt32(2);
            string tagText = dataRead.GetString(3);
            string description = dataRead.GetString(4);
            string icon = dataRead.GetString(5);
            string sprite = dataRead.GetString(6);

            string[] iconSplit = icon.Split(',');
            int[] iconValues = new int[iconSplit.Length];

            for (int i = 0; i < iconSplit.Length; i++)
            {
                iconValues[i] = int.Parse(iconSplit[i]);
            }

            deck.cards.Add(new Card(title, description, type, (TagType)tag, tagText, iconValues, sprite) { iconValues = iconValues, SpritePath = sprite});

        }

        Close();
        currentDeck = deck;
        return deck;

    }

    public void DeleteDeck(int ID, string name)
    {
        Open();
        //TODO hav noget confirmation 
        var cmd = new SqliteCommand($"DELETE FROM Deck WHERE ID={ID} AND Name={name}", (SqliteConnection)connection);

       // cmd.ExecuteNonQuery();

        Close();
    }
}
