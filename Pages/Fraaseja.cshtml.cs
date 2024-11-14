using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace sanakirja.Pages;

public class FraasejaModel : PageModel
{

    public SortedDictionary<string, string> data;
    private readonly ILogger<FraasejaModel> _logger;

    public FraasejaModel(ILogger<FraasejaModel> logger)
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
                                    from fraaseja
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
