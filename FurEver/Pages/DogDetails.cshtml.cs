using FurEver.API_Data;
using FurEver.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;

namespace FurEver.Pages
{
    public class DogDetailsModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string? breedName { get; set; }

        [BindProperty(SupportsGet = true)]
        public API_Data.DogBreed? DogDetails{ get; set; }

        [BindProperty(SupportsGet = true)]
        public string? RecommendedMessage { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(breedName))
            {
                return RedirectToPage("/Error");
            }

            var allDogBreeds = await DogBreedService.FetchBreedsAsync();

            // Try to fetch by Name first
            DogDetails = allDogBreeds.FirstOrDefault(breed => breed?.General?.Name == breedName);
            //DogDetails = allDogBreeds.Where(row => row?.General?.Name == breedName).FirstOrDefault();

            // If not found, try fetching by Id (assuming Id is a string property in DogBreed.General)
            if (DogDetails == null)
            {
                DogDetails = allDogBreeds.FirstOrDefault(breed => breed?.General?.Id == breedName);
            }

            // If still not found, return Not Found
            if (DogDetails == null)
            {
                return NotFound();
            }
            return Page();
        } 
    }
}
