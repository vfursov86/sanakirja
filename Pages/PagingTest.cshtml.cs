using Microsoft.AspNetCore.Mvc.RazorPages;
using JW;
using datastore;

namespace sanakirja.Pages;

public class PagingTestModel : PageModel
{
    // This is the refernce to pikkusanatData in DataStore static class.
    public SortedDictionary<string, string> data;
    private readonly ILogger<PagingTestModel> _logger;
    public Pager Pager { get; set; }

    public IEnumerable<KeyValuePair<string, string>> Items { get; set; }

    public PagingTestModel(ILogger<PagingTestModel> logger)
    {
        data = DataStore.pikkusanatData;
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

