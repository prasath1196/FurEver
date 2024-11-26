using FurEver.API_Data;
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

            var apiResult = await DogBreedService.FetchBreedsAsync();

            if (apiResult != null)
            {
                foreach (var breed in apiResult)
                {
                    DogBreeds.Add(breed.General.Name);
                }
            }
            
        }
    }
}
