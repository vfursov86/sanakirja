using Microsoft.Data.Sqlite;


namespace datastore;

public static class DataStore
{
    // Finnish-sorted dictionaries
    public static SortedDictionary<string, string> sanastoData = new();
    public static SortedDictionary<string, string> pikkusanatData = new();
    public static SortedDictionary<string, string> fraasejaData = new();

    // Russian-sorted dictionaries
    public static SortedDictionary<string, string> sanastoDataRussianSorted = new();
    public static SortedDictionary<string, string> pikkusanatDataRussianSorted = new();
    public static SortedDictionary<string, string> fraasejaDataRussianSorted = new();

    // Verbi Rektio dictionary
    public static SortedDictionary<string, string> verbirektioData = new();


    public static void LoadData()
    {
        using (var connection = new SqliteConnection("Data Source=sanakirjadata.db"))
        {
            connection.Open();

            // Load Finnish-sorted data
            LoadTableData(connection, "finnruswords", sanastoData, sanastoDataRussianSorted);
            LoadTableData(connection, "pikkusanat", pikkusanatData, pikkusanatDataRussianSorted);
            LoadTableData(connection, "fraaseja", fraasejaData, fraasejaDataRussianSorted);

            // Load verbirektio examples
            LoadVerbiRektioData(connection);


        }
    }

    private static void LoadTableData(SqliteConnection connection, string tableName, SortedDictionary<string, string> finnishSorted, SortedDictionary<string, string> russianSorted)
    {
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT finnish, russian FROM {tableName}";
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var finn = reader.GetString(0);
                var rus = reader.GetString(1);

                finnishSorted.Add(finn, rus); // Finnish-sorted
                russianSorted.Add(rus, finn); // Russian-sorted
            }
        }
    }

    private static void LoadVerbiRektioData(SqliteConnection connection)
    {
        var command = connection.CreateCommand();
        command.CommandText = "SELECT verbirektioitem, usageexample FROM verbirektio";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var item = reader.GetString(0);
            var example = reader.GetString(1);

            // SortedDictionary will auto-sort by item
            verbirektioData.Add(item, example);
        }
    }

}
