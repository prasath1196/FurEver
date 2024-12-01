using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace FurEver.Pages
{
    public class SearchBooksModel : PageModel
    {

        public string SearchQuery { get; set; }
        public List<string> BookResults { get; private set; } = new List<string>();
        public static List<string> Authors { get; private set; } = new List<string>();

        public async Task OnGetAsync(string searchQuery)
        {
            SearchQuery = searchQuery;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Fetch books by the author
                await FetchBooksByAuthorAsync(searchQuery);
            }
            else
            {
                // Fetch authors only when page loads initially
                if (!Authors.Any())
                {
                    await FetchAuthorsAsync();
                }
            }
        }

        private async Task FetchAuthorsAsync()
        {
            var apiUrl = "https://openlibrary.org/search.json?q=authors";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(jsonResponse);

                    // Extract unique author names from the response
                    var docs = jsonDocument.RootElement.GetProperty("docs");
                    var authorSet = new HashSet<string>();

                    foreach (var doc in docs.EnumerateArray())
                    {
                        if (doc.TryGetProperty("author_name", out var authors))
                        {
                            foreach (var author in authors.EnumerateArray())
                            {
                                authorSet.Add(author.GetString());
                            }
                        }
                    }

                    Authors = authorSet.ToList();
                }
            }
        }

        private async Task FetchBooksByAuthorAsync(string author)
        {
            var apiUrl = $"https://openlibrary.org/search.json?author={author}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(jsonResponse);

                    // Extract book titles for the specified author
                    var docs = jsonDocument.RootElement.GetProperty("docs");
                    var bookList = new List<string>();

                    foreach (var doc in docs.EnumerateArray())
                    {
                        if (doc.TryGetProperty("title", out var title))
                        {
                            bookList.Add(title.GetString());
                        }
                    }

                    BookResults = bookList;
                }
            }
        }

        // API endpoint to get author names (used by JavaScript)
        public IActionResult OnGetAuthors()
        {
            return new JsonResult(Authors);
        }
    }
}
