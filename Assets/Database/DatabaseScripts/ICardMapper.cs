using Mono.Data.Sqlite;
using System.Collections.Generic;

public interface ICardMapper
{
    List<CardData> MapCardsFromReader(SqliteDataReader reader);
}

