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
        _logger = logger;
    }

    public void OnGet(string sortOrder = "finnish", int p = 1)
    {
        // Select the correct dictionary based on sort order
        var data = sortOrder == "finnish" ? DataStore.sanastoData : DataStore.sanastoDataRussianSorted;

        // Get pagination info for the current page. Yield 15 records and show 4 pages in submenu.
        Pager = new Pager(data.Count(), p, 15, 4);
        // Assign a current slice of data to Items to display.
        Items = data.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize);
    }
}
