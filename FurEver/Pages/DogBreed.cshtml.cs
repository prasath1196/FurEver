using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using FurEver.API_Data;

namespace FurEver.Pages
{
    public class DogBreedModel : PageModel
    {


        private readonly ILogger<DogBreedModel> _logger;
        public List<API_Data.DogBreed>? DogBreeds { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } // Property to hold the search term
        public DogBreedModel(ILogger<DogBreedModel> logger)
        {
            _logger = logger;
        }
        public async Task OnGetAsync()
        {
            try
            {
                DogBreeds = await DogBreedService.FetchBreedsAsync();
                if (!string.IsNullOrEmpty(SearchTerm))
                {
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