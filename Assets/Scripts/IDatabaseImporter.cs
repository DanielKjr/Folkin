using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public interface IDatabaseProvider
{
    /// <summary>
    /// Establish connection to database
    /// </summary>
    /// <returns></returns>
    IDbConnection CreateConnection();
  
}
