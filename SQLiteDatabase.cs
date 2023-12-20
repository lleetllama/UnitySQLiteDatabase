using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

/// <summary>
/// Represents the game data and provides methods to interact with the game's database.
/// </summary>
public class SQLiteDatabase : MonoBehaviour
{
    private static SQLiteDatabase _instance;

    /// Singleton instance accessor.
    public static SQLiteDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SQLiteDatabase>();
            }
            return _instance;
        }
    }

    private string connectionString;

    /// <summary>
    /// Called before the first frame update.
    /// Checks if an instance of the script already exists and destroys the duplicate if found.
    /// Sets the instance to this script and prevents it from being destroyed when loading a new scene.
    /// Sets the connection string for the game data database.
    /// </summary>
    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        connectionString = "URI=file:" + Application.dataPath + "/Databases/GameData.db";
    }

    /// <summary>
    /// Executes the specified SQL query on the database.
    /// </summary>
    /// <param name="sqlQuery">The SQL query to execute.</param>
    public void ExecuteQuery(string sqlQuery)
    {
        try
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqlQuery;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error during database operation: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves data from a database table based on the provided SQL query.
    /// </summary>
    /// <param name="sqlQuery">The SQL query to execute.</param>
    /// <returns>A dictionary containing the retrieved data, with table names as keys and lists of objects as values.</returns>
    public Dictionary<string, List<object>> GetDataTable(string sqlQuery)
    {
        Dictionary<string, List<object>> resultDictionary = new Dictionary<string, List<object>>();

        try
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqlQuery;

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        do
                        {
                            string tableName = reader.GetSchemaTable().TableName;

                            if (!resultDictionary.ContainsKey(tableName))
                            {
                                resultDictionary[tableName] = new List<object>();
                            }

                            while (reader.Read())
                            {
                                object[] values = new object[reader.FieldCount];
                                reader.GetValues(values);
                                resultDictionary[tableName].Add(values);
                            }
                        } while (reader.NextResult());
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error during database operation: {ex.Message}");
        }

        return resultDictionary;
    }
}
