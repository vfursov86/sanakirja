using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using datastore;

namespace sanakirja.Pages;

public class SanastoModel : PageModel
{
    // This is the refernce to sanastoData in DataStore static class.
    public SortedDictionary<string, string> data;
    private readonly ILogger<SanastoModel> _logger;

    public SanastoModel(ILogger<SanastoModel> logger)
    {
        data = new SortedDictionary<string , string>();
        _logger = logger;
    }

    public void OnGet()
    {
        data = DataStore.sanastoData;
    }
}

// create table finnruswords(id int primary key not null, finnish text not null, russian text not null);