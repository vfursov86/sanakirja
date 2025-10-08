using Microsoft.AspNetCore.Mvc.RazorPages;
using JW;
using datastore;

namespace sanakirja.Pages;

public class VerbiRektioModel : PageModel
{
    private readonly ILogger<VerbiRektioModel> _logger;
    public Pager Pager { get; set; }
    public IEnumerable<KeyValuePair<string, string>> Items { get; set; }

    public VerbiRektioModel(ILogger<VerbiRektioModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(int p = 1)
    {
        var data = DataStore.verbirektioData;

        // Create pager: 15 items per page, 4 pages in submenu
        Pager = new Pager(data.Count(), p, 15, 4);

        // Slice the data for current page
        Items = data.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize);
    }
}
