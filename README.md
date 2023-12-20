# SQLiteDatabase Unity Script

## Overview

The `SQLiteDatabase` script is designed to represent game data and provides methods to interact with a SQLite database in Unity. It can execute SQL queries and retrieve data from the game's database.

## Features

- Singleton design pattern to ensure a single instance of the script.
- Methods for executing SQL queries and retrieving data from the database.
- Integration with SQLite for Unity.

## Getting Started

### Prerequisites
- Create a directory named "Plugins" within your "Assets" folder.
- Visit the SQLite Download Page and download the appropriate binaries for your project.
- Extract the contents of the downloaded zip file. Inside, you'll find a DLL file and a DEF file. Copy both of these files into the newly created "Plugins" folder.
- Find Mono.Data.Sqlite.dll. This file can be found in your Unity editor folder. For instance, in my case, it was situated here: C:\Program Files\Unity\Hub\Editor\2021.3.1f1\Editor\Data\MonoBleedingEdge\lib\mono\unity.
Once you've located the Mono.Data.Sqlite.dll file, copy it and paste it into the "Plugins" folder alongside the previously added files.

### Installation

1. Copy the `SQLiteDatabase.cs` script into your Unity project's `Scripts` folder.
2. Attach the `SQLiteDatabase` script to an empty GameObject in your scene.

### Database Setup

1. Create a new SQLite database file (`GameData.db`).
2. Define your database schema and tables.
3. Update the connection string in `SQLiteDatabase.cs` to point to your database file.

### Usage

1. **Executing Queries:**
    - Use the `ExecuteQuery` method to execute SQL queries that modify the database.

    ```csharp
    SQLiteDatabase.Instance.ExecuteQuery("CREATE TABLE IF NOT EXISTS ...");
    ```

2. **Retrieving Data:**
    - Use the `GetDataTable` method to retrieve data from the database.

    ```csharp
    Dictionary<string, List<object>> result = SQLiteDatabase.Instance.GetDataTable("SELECT * FROM ...");
    ```

3. **Singleton Instance:**
    - Access the `SQLiteDatabase` instance using the `Instance` property.

    ```csharp
    SQLiteDatabase.Instance.ExecuteQuery("INSERT INTO ...");
    ```

## Example

```csharp
// Insert data into the Player table
string insertQuery = "INSERT INTO Player (name, score) VALUES ('Player1', 100)";
SQLiteDatabase.Instance.ExecuteQuery(insertQuery);

// Retrieve data from the Player table
string selectQuery = "SELECT * FROM Player";
Dictionary<string, List<object>> playerData = SQLiteDatabase.Instance.GetDataTable(selectQuery);
