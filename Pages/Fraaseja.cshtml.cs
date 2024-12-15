using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using datastore;

namespace sanakirja.Pages;

public class FraasejaModel : PageModel
{

    // This is the refernce to fraasejaData in DataStore static class.
    public SortedDictionary<string, string> data;
    private readonly ILogger<FraasejaModel> _logger;

    public FraasejaModel(ILogger<FraasejaModel> logger)
    {
        data = new SortedDictionary<string , string>();
        _logger = logger;
    }

    public void OnGet()
    {
        data = DataStore.fraasejaData;
    }
}
