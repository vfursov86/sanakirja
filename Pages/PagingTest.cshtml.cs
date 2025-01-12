using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JW;
using datastore;

namespace sanakirja.Pages;

public class PagingTestModel : PageModel
{
    // This is the refernce to pikkusanatData in DataStore static class.
    public SortedDictionary<string, string> data;
    private readonly ILogger<PagingTestModel> _logger;
    public Pager Pager { get; set; }

    public IEnumerable<string> Items { get; set; }

    public PagingTestModel(ILogger<PagingTestModel> logger)
    {
        data = new SortedDictionary<string , string>();
        _logger = logger;
    }

    public void OnGet(int p = 1)
    {
        // generate list of sample items to be paged
        var dummyItems = Enumerable.Range(1, 150).Select(x => "Item " + x);

        // get pagination info for the current page
        Pager = new Pager(dummyItems.Count(), p, 20, 6);

            // assign the current page of items to the Items property
        Items = dummyItems.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize);

        //data = DataStore.pikkusanatData;

        
    }
}

