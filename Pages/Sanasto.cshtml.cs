using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace sanakirja.Pages;

public class SanastoModel : PageModel
{
    // Here will be stored words while runtime.
    public SortedDictionary<string, string> data;
    private readonly ILogger<SanastoModel> _logger;

    public SanastoModel(ILogger<SanastoModel> logger)
    {
        data = new SortedDictionary<string , string>();
        _logger = logger;
    }

    public void OnGet()
    {
        using (var connection = new SqliteConnection("Data Source=sanakirjadata.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
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
                    
                    data.Add(finn, rus);
                }
            }
        }
    }
}

// create table finnruswords(id int primary key not null, finnish text not null, russian text not null);