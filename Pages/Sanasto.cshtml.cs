using Microsoft.AspNetCore.Mvc.RazorPages;
using JW;
using datastore;

namespace sanakirja.Pages;

public class SanastoModel : PageModel
{
    // This is the refernce to sanastoData in DataStore static class.
    public SortedDictionary<string, string> data;
    private readonly ILogger<SanastoModel> _logger;
    public Pager Pager { get; set; }
    public IEnumerable<KeyValuePair<string, string>> Items { get; set; }

    public SanastoModel(ILogger<SanastoModel> logger)
    {
        data = DataStore.sanastoData;
        _logger = logger;
    }

    public void OnGet(int p = 1)
    {
        // Get pagination info for the current page. Yield 15 records and show 4 pages in  the submenu.
        Pager = new Pager(data.Count(), p, 15, 4);
        // Assign a current slice of data to Items to display.
        Items = data.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize);
    }
}

// create table finnruswords(id int primary key not null, finnish text not null, russian text not null);