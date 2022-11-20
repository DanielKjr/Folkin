using Mono.Data.Sqlite;
using UnityEditor.MemoryProfiler;

public class DeckManager : DatabaseHandler
{

    private Deck currentDeck;





    /// <summary>
    /// Loads the deck from database with the corresponding ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public Deck LoadDeck(int ID, DatabaseHandler handler)
    {
        Deck deck = new Deck();

        var cmd = new SqliteCommand($"SELECT * FROM Card WHERE DeckID={ID}", (SqliteConnection)handler.Connection);
        var dataRead = cmd.ExecuteReader();

        while (dataRead.Read())
        {
            var title = dataRead.GetString(2);
            var type = dataRead.GetString(3);
            var tag = dataRead.GetInt32(4);
            var tagText = dataRead.GetString(5);
            var description = dataRead.GetString(6);
            var icon = dataRead.GetString(7);
            var sprite = dataRead.GetString(8);


            string[] iconSplit = icon.Split(',');
            int[] iconValues = new int[iconSplit.Length];

            for (int i = 0; i < iconSplit.Length; i++)
            {
                if (char.IsDigit(iconSplit[i][i]))
                {
                    iconValues[i] = int.Parse(iconSplit[i]);
                }
               
            }

            deck.CardDatas.Add(new CardData(title, description, type, (TagType)tag, tagText, iconValues, sprite));

        }

        cmd = new SqliteCommand($"SELECT Name FROM Deck WHERE UserID='{ID}'", (SqliteConnection)handler.Connection);
        dataRead = cmd.ExecuteReader();

        while (dataRead.Read())
        {
            deck.Name = dataRead.GetString(0);
        }
        
        
        deck.ID = ID;     
        currentDeck = deck;
        return deck;

    }

    public void SaveDeckToDb(Deck deck, DatabaseHandler handler)
    {
      

        var cmd = new SqliteCommand($"DELETE FROM Card WHERE DeckID='{deck.ID}'", (SqliteConnection)handler.Connection);
        cmd.ExecuteNonQuery();

        cmd = new SqliteCommand($"DELETE FROM Deck WHERE ID='{deck.ID}'", (SqliteConnection)handler.Connection);
        cmd.ExecuteNonQuery();

        cmd = new SqliteCommand($"INSERT INTO Deck (ID, UserID, Name) VALUES (null, '{deck.ID}', '{deck.Name}')",(SqliteConnection)handler.Connection);
        cmd.ExecuteNonQuery();

        foreach (CardData card in deck.CardDatas)
        {
            handler.CManager.SaveCardToDb(deck.ID, card, handler);
        }

   
    }


    public void DeleteDeck(int ID, string name, DatabaseHandler handler)
    {

        //TODO hav noget confirmation 
        var cmd = new SqliteCommand($"DELETE FROM Deck WHERE ID={ID} AND Name={name}", (SqliteConnection)handler.Connection);
        cmd.ExecuteNonQuery();

        cmd = new SqliteCommand($"DELETE FROM Card WHERE DeckID='{ID}'", (SqliteConnection)handler.Connection);
        cmd.ExecuteNonQuery();


    }

    /// <summary>
    /// NOT USABLE AT THE MOMENT
    /// </summary>
    /// <param name="deck"></param>
    /// <param name="handler"></param>
    public void DeleteDeck(Deck deck, DatabaseHandler handler)
    {
        Open();
        //TODO hav noget confirmation 
        var cmd = new SqliteCommand($"DELETE FROM Deck WHERE ID='{deck.ID}' AND Name='{deck.Name}'", (SqliteConnection)handler.Connection);
        cmd.ExecuteNonQuery();
        foreach (Card card in deck.cards)
        {
            CManager.DeleteFromDatabase(card, handler);
        }

       

        Close();
    }
}
