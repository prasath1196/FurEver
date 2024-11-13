using FurEver.Data;
using FurEver.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace FurEver.Pages
{
    public class ReviewPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReviewPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Review Review { get; set; } = new Review();

        public List<string> DogBreeds { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            await LoadDogBreedsAsync();
        }

        public string? Message { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                await LoadDogBreedsAsync();
                return Page();
            }

            // Add the review data to the database
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            Message = "Thank you for your story! It has been saved successfully.";

            
            return Page(); 
        }

        private async Task LoadDogBreedsAsync()
        {
            var apiUrl = "https://registry.dog/api/v1"; 

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var apiResult = JsonSerializer.Deserialize<ApiResult>(jsonResponse, options);

                    if (apiResult != null && apiResult.Status == "success")
                    {
                        foreach (var breed in apiResult.Data)
                        {
                            DogBreeds.Add(breed.General.Name);
                        }
                    }
                }
            }
        }

        public class ApiResult
        {
            public string? Status { get; set; }
            public List<DogBreed> Data { get; set; }
        }

        public class DogBreed
        {
            public GeneralInfo General { get; set; }
        }

        public class GeneralInfo
        {
            public string Name { get; set; }
        }
    }
}
