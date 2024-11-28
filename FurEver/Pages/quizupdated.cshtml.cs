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
        public string LivingEnvironment { get; set; }
        [BindProperty]
        public string ActivityLevel { get; set; }
        [BindProperty]
        public string FamilySituation { get; set; }
        [BindProperty]
        public string GroomingPreference { get; set; }
        [BindProperty]
        public string DogSize { get; set; }
        [BindProperty]
        public string BarkingTolerance { get; set; }
        [BindProperty]
        public string ExerciseCommitment { get; set; }
        [BindProperty]
        public string TrainingDifficultyPreference { get; set; }
        [BindProperty]
        public string LifespanPreference { get; set; }

        public List<DogBreed> DogBreeds { get; set; } = new List<DogBreed>();

        public async Task OnGet()
        {
            // Fetch dog breeds from the API
            DogBreeds = await DogBreedService.FetchBreedsAsync();
        }

        public void OnPost()
        {
            // Fetch dog breeds again to apply the filter
            DogBreeds = DogBreedService.FetchBreedsAsync().Result;

            // Filter breeds based on the user's responses
            var filteredBreeds = DogBreeds.Where(b =>
                (LivingEnvironment == "Apartment" && b.Physical?.Size == 1) ||
                (LivingEnvironment == "House" && b.Physical?.Size > 1) &&
                (ActivityLevel == "Active" && b.Physical?.Size == 3) ||
                (ActivityLevel == "Moderate" && b.Physical?.Size == 2) ||
                (ActivityLevel == "Low" && b.Physical?.Size == 1) &&
                (FamilySituation == "ChildrenAndPets" && b.Behavior?.FamilyAffection > 7) ||
                (FamilySituation == "NoChildrenNoPets" && b.Behavior?.FamilyAffection < 5) &&
                (GroomingPreference == "Low" && b.Physical?.CoatLength < 3) ||
                (GroomingPreference == "High" && b.Physical?.CoatLength > 3) &&
                (DogSize == "Small" && b.General?.Height < 15) ||
                (DogSize == "Medium" && b.General?.Height >= 15 && b.General?.Height < 25) ||
                (DogSize == "Large" && b.General?.Height >= 25) &&
                (BarkingTolerance == "Low" && b.Behavior?.BarkingFrequency < 4) ||
                (BarkingTolerance == "Moderate" && b.Behavior?.BarkingFrequency >= 4 && b.Behavior?.BarkingFrequency < 7) ||
                (BarkingTolerance == "High" && b.Behavior?.BarkingFrequency >= 7) &&
                (ExerciseCommitment == "Low" && b.Care?.ExerciseNeeds < 4) ||
                (ExerciseCommitment == "Moderate" && b.Care?.ExerciseNeeds >= 4 && b.Care?.ExerciseNeeds < 7) ||
                (ExerciseCommitment == "High" && b.Care?.ExerciseNeeds >= 7) &&
                (TrainingDifficultyPreference == "Easy" && b.Care?.TrainingDifficulty < 4) ||
                (TrainingDifficultyPreference == "Moderate" && b.Care?.TrainingDifficulty >= 4 && b.Care?.TrainingDifficulty < 7) ||
                (TrainingDifficultyPreference == "Challenging" && b.Care?.TrainingDifficulty >= 7) &&
                (LifespanPreference == "Short" && b.General?.Lifespan < 10) ||
                (LifespanPreference == "Average" && b.General?.Lifespan >= 10 && b.General?.Lifespan <= 15) ||
                (LifespanPreference == "Long" && b.General?.Lifespan > 15)
            ).ToList();

            if (filteredBreeds.Any())
            {
                // Recommend a breed based on the filtered list
                var recommendedBreed = filteredBreeds.First();
                RecommendedBreed = recommendedBreed.General?.Name;
                BreedImage = recommendedBreed.Images?.Small?.Studio ?? recommendedBreed.Images?.Large?.Studio ?? "/images/default-dog.png";
            }
            else
            {
                ErrorMessage = "No breed matches your preferences. Please try again.";
            }
        }
    }
}