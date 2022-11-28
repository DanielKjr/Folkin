using Mono.Data.Sqlite;
using System.Collections.Generic;

public interface ICardMapper
{
    /// <summary>
    /// Returns the result of an sqlite query in form of a list of CardData
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    List<CardData> MapCardsFromReader(SqliteDataReader reader);
}

