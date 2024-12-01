using FurEver.API_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurEver.Pages
{
    public class DogBreedModel : PageModel
    {
        private readonly ILogger<DogBreedModel> _logger;

        // List of dog breeds
        public List<DogBreed>? DogBreeds { get; set; }

        // Property to bind the search term
        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public DogBreedModel(ILogger<DogBreedModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                // Fetch all dog breeds from the service
                DogBreeds = await DogBreedService.FetchBreedsAsync();

                // If a search term is provided, filter the breeds
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    // Perform a case-insensitive search for breeds by name
                    DogBreeds = DogBreeds
                        .Where(breed => breed.General.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                _logger.LogInformation("Seserialised response: {Response}", DogBreeds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching data");
                DogBreeds = new List<API_Data.DogBreed>();
            }
        }
    }
}
