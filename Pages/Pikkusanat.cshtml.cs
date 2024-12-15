using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using datastore;

namespace sanakirja.Pages;

public class PikkusanatModel : PageModel
{
    // This is the refernce to pikkusanatData in DataStore static class.
    public SortedDictionary<string, string> data;
    private readonly ILogger<PikkusanatModel> _logger;

    public PikkusanatModel(ILogger<PikkusanatModel> logger)
    {
        data = new SortedDictionary<string , string>();
        _logger = logger;
    }

    public void OnGet()
    {
        data = DataStore.pikkusanatData;
    }
}

