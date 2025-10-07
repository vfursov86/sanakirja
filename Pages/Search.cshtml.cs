using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using datastore;

namespace sanakirja.Pages;

public class SearchModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Query { get; set; }

    public List<(string Finnish, string Russian)> Results { get; set; } = new();

    public void OnGet()
    {
        if (string.IsNullOrWhiteSpace(Query)) return;

        var q = Query.Trim().ToLower();
        var seen = new HashSet<string>();

        void AddPair(string finn, string rus)
        {
            var key = $"{finn}|{rus}";
            var reverseKey = $"{rus}|{finn}";

            if (!seen.Contains(key) && !seen.Contains(reverseKey))
            {
                Results.Add((finn, rus));
                seen.Add(key);
            }
        }

        void SearchDict(SortedDictionary<string, string> dict)
        {
            foreach (var kvp in dict)
            {
                if (kvp.Key.ToLower().Contains(q) || kvp.Value.ToLower().Contains(q))
                {
                    AddPair(kvp.Key, kvp.Value);
                }
            }
        }

        // Search all six dictionaries
        SearchDict(DataStore.sanastoData);
        SearchDict(DataStore.sanastoDataRussianSorted);
        SearchDict(DataStore.pikkusanatData);
        SearchDict(DataStore.pikkusanatDataRussianSorted);
        SearchDict(DataStore.fraasejaData);
        SearchDict(DataStore.fraasejaDataRussianSorted);
    }

}
