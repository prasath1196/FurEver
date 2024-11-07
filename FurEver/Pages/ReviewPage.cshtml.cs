using FurEver.Data;
using FurEver.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace FurEver.Pages
{
    public class ReviewPageModel : PageModel
    {
        public List<string> DogBreeds { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            await LoadDogBreedsAsync();
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
