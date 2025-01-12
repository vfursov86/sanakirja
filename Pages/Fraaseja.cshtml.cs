using Microsoft.AspNetCore.Mvc.RazorPages;
using JW;
using datastore;

namespace sanakirja.Pages;

public class FraasejaModel : PageModel
{

    // This is the refernce to fraasejaData in DataStore static class.
    public SortedDictionary<string, string> data;
    private readonly ILogger<FraasejaModel> _logger;
    public Pager Pager { get; set; }
    public IEnumerable<KeyValuePair<string, string>> Items { get; set; }

    public FraasejaModel(ILogger<FraasejaModel> logger)
    {
        data = DataStore.fraasejaData;
        _logger = logger;
    }

    public void OnGet(int p = 1)
    {
        // Get pagination info for the current page. Yield 15 records and show 4 pages in submenu.
        Pager = new Pager(data.Count(), p, 15, 4);
        // Assign a current slice of data to Items to display.
        Items = data.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize);
    }
}
