using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using FurEver.API_Data;
using Newtonsoft.Json;

namespace FurEver.Pages
{
    public class SearchBooksModel : PageModel
    {

        public string SearchQuery { get; set; }
        public List<Book> BookResults { get; private set; } = new List<Book>();
        public static List<string> Authors { get; private set; } = new List<string>();

        public async Task OnGetAsync(string searchQuery)
        {
               
            await FetchBooks(searchQuery);
                
           
        }

        private async Task FetchBooks(string author)
        {
            var apiUrl = "https://mindlift20241130171555.azurewebsites.net/api/reviews";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(jsonResponse);
                    
                    // Extract book titles for the specified author
                    var docs = jsonDocument.RootElement;
                    var bookList = new List<Book>();
                    bookList = JsonConvert.DeserializeObject<List<Book>>(jsonResponse);
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

    public class Book 
    { 
    
        [JsonProperty("bookTitle")]
        public string BookTitle { get; set; }
        
        [JsonProperty("comment")]
        public string BookReview { get; set; }
    }
}
