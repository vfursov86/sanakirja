using Microsoft.Data.Sqlite;


namespace datastore;

public static class DataStore
{
    // Stores words for Sanasto page.
    public static SortedDictionary<string, string> sanastoData = new();
    // Stores words for Pikkusanat page.
    public static SortedDictionary<string, string> pikkusanatData = new();
    // Keeps data for Fraaseja page
    public static SortedDictionary<string, string> fraasejaData = new();
    public static void LoadData()
    {
        using (var connection = new SqliteConnection("Data Source=sanakirjadata.db"))
        {
            connection.Open();

            LoadTableData(connection, "finnruswords", sanastoData);
            LoadTableData(connection, "pikkusanat", pikkusanatData);
            LoadTableData(connection, "fraaseja", fraasejaData);
        }    
    }

    private static void LoadTableData(SqliteConnection connection, string tableName, SortedDictionary<string, string> dataDictionary)
    {
        var command = connection.CreateCommand();
        command.CommandText = $@"select id, finnish, russian from {tableName}";

        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var finn = reader.GetString(1);
                var rus = reader.GetString(2);

                dataDictionary.Add(finn, rus);
            }
        }
    }
}