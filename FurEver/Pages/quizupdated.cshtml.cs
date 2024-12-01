using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FurEver.API_Data;
using FurEver.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FurEver.Pages
{
    public class QuizModel : PageModel
    {
        public string? RecommendedBreed { get; set; }
        public string BreedImage { get; set; }
        public string ErrorMessage { get; set; }

        [BindProperty]
        public int Friendliness { get; set; } // New user preference
        [BindProperty]
        public int Adaptability { get; set; } // New user preference
        [BindProperty]
        public int GroomingPreference { get; set; }
        [BindProperty]
        public int ActivityLevel { get; set; }
        [BindProperty]
        public int BarkingTolerance { get; set; }
        [BindProperty]
        public int TrainingDifficultyPreference { get; set; }
        [BindProperty]
        public int FriendlinessToStrangers { get; set; }
        [BindProperty]
        public int LifespanPreference { get; set; }

        public List<DogBreed> DogBreeds { get; set; } = new List<DogBreed>();

        public async Task OnGet()
        {
            // Fetch dog breeds from the API
            DogBreeds = await DogBreedService.FetchBreedsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Fetch dog breeds from the API
            DogBreeds = await DogBreedService.FetchBreedsAsync();

            // Filter breeds based on numeric preferences
            var filteredBreeds = DogBreeds.Where(b =>
                (b.Behavior?.FamilyAffection ?? 0) >= Friendliness &&
                (b.Behavior?.Adaptability ?? 0) >= Adaptability &&
                (b.Physical?.CoatLength ?? 0) <= GroomingPreference &&
                (b.Care?.ExerciseNeeds ?? 0) >= ActivityLevel &&
                (b.Behavior?.BarkingFrequency ?? 0) <= BarkingTolerance &&
                (b.Care?.TrainingDifficulty ?? 0) <= TrainingDifficultyPreference &&
                (b.Behavior?.FriendlinessToStrangers ?? 0) >= FriendlinessToStrangers &&
                (b.General?.Lifespan ?? 0) >= LifespanPreference
            ).ToList();

            if (filteredBreeds.Any())
            {
                var recommendedBreed = filteredBreeds.First();
                RecommendedBreed = recommendedBreed.General?.Name;
                BreedImage = recommendedBreed.Images?.Small?.Studio ?? recommendedBreed.Images?.Large?.Studio ?? "/images/default-dog.png";
            }
            else
            {
                ErrorMessage = "No breed matches your preferences. Please try again.";
            }

            return Page();
        }
    }
}
