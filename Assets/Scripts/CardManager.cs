using Mono.Data.Sqlite;
using UnityEditor.U2D.Common;

public class CardManager : DatabaseHandler
{
    private static CardManager instance;
    private Card currentCard;

    public static CardManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new CardManager();
            }
            return instance;
        }
    }

   

    protected Card FindFromDb(string cardName, int ID)
    {

        Open();

        var cmd = new SqliteCommand($"SELECT Title, Type, Tag, Description, Icon FROM 'Card' WHERE DeckID={ID} AND Title={cardName}", (SqliteConnection)connection);
        var dataRead = cmd.ExecuteReader();


        string title = dataRead.GetString(0);
        string type = dataRead.GetString(1);
        int tag = dataRead.GetInt32(2);
        string description = dataRead.GetString(3);
        int icon = dataRead.GetInt32(4);

        //  Card card = new Card(title, description, type, tag, icon);
        Card card = new Card();

        currentCard = card;

        return card;
    }

    public void EditCard(Card? card)
    {

    }

    public void SaveToDb(int ID)
    {
        if (currentCard != null)
        {
            var cmd = new SqliteCommand($"INSERT INTO Card (ID, DeckID, Title, Type, Tag, Description, Icon" +
                $"VALUES '{currentCard.ID}','{ID}', '{currentCard.titleText}'");//mangler resten men der er ikke enums og properties endnu
        }
    }


}
