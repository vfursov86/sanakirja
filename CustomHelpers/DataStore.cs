using Microsoft.Data.Sqlite;


namespace datastore;

public static class DataStore
{
    // Stores words for Sanasto page.
    public static SortedDictionary<string, string> sanastoData = new();
    // Stores words for Pikkusanat page.
    public static SortedDictionary<string, string> pikkusanatData = new();
    // Keeps data for Fraaseja page.
    public static SortedDictionary<string, string> fraasejaData = new();
    public static void LoadData()
    {
        using (var connection = new SqliteConnection("Data Source=sanakirjadata.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();

            // Load from finnruswords table and set them to _sanastoData dictionary.
            command.CommandText = @"
                                    select id, finnish, russian
                                    from finnruswords
                                ";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetString(0);
                    var finn = reader.GetString(1);
                    var rus = reader.GetString(2);
                    
                    sanastoData.Add(finn, rus);
                }
            }
            // Load from pikkusanat table and set them to _pikkusanatData dictionary.
            command.CommandText = @"
                                    select id, finnish, russian
                                    from pikkusanat
                                ";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetString(0);
                    var finn = reader.GetString(1);
                    var rus = reader.GetString(2);
                    
                    pikkusanatData.Add(finn, rus);
                }
            }

            command.CommandText = @"
                                    select id, finnish, russian
                                    from fraaseja
                                ";
            // Load from fraaseja table and set them to _fraasejaData dictionary.
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetString(0);
                    var finn = reader.GetString(1);
                    var rus = reader.GetString(2);

                    fraasejaData.Add(finn, rus);
                }
            }                   
        }
    }
}