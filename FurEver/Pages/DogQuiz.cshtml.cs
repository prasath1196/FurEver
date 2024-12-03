using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace FurEver.Pages
{
    public class DogQuizModel : PageModel
    {
        public string QuizImageUrl { get; set; }
        public List<string> BreedOptions { get; set; }
        public string AnswerResult { get; set; }
        public bool QuizFinished { get; set; } = false;

        [TempData]
        public int Score { get; set; }

        [TempData]
        public int CurrentQuestionNumber { get; set; } = 1;

        [TempData]
        public string CorrectBreed { get; set; }  // Store correct breed in TempData

        public readonly int TotalQuestions = 5;

        private static readonly string[] DogBreeds =
        {
            "beagle", "bulldog", "dalmatian", "goldenretriever", "labrador", "poodle", "pug", "shiba", "husky", "germanshepherd"
        };

        private static List<string> QuizImages { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            // Check if quiz is complete
            if (CurrentQuestionNumber > TotalQuestions)
            {
                QuizFinished = true;
                return;
            }

            // Load unique images if not already loaded
            if (!QuizImages.Any())
            {
                using (var httpClient = new HttpClient())
                {
                    while (QuizImages.Count < TotalQuestions)
                    {
                        var response = await httpClient.GetStringAsync("https://dog.ceo/api/breeds/image/random");
                        var jsonResponse = JObject.Parse(response);
                        var imageUrl = jsonResponse["message"]?.ToString();

                        if (imageUrl != null && !QuizImages.Contains(imageUrl))
                        {
                            QuizImages.Add(imageUrl);
                        }
                    }
                }
            }

            // Set the current image and breed details
            if (QuizImages.Count >= CurrentQuestionNumber)
            {
                QuizImageUrl = QuizImages[CurrentQuestionNumber - 1];
                CorrectBreed = QuizImageUrl.Split('/')[4]; // Extract breed from URL
                TempData["CorrectBreed"] = CorrectBreed;   // Store correct breed in TempData

                // Generate breed options with the correct answer included
                BreedOptions = DogBreeds.OrderBy(x => System.Guid.NewGuid()).Take(3).ToList();
                if (!BreedOptions.Contains(CorrectBreed))
                {
                    BreedOptions[0] = CorrectBreed; // Ensure correct breed is included
                }
                BreedOptions = BreedOptions.OrderBy(x => System.Guid.NewGuid()).ToList(); // Shuffle options
            }
        }

        public async Task<IActionResult> OnPostAsync(string selectedBreed)
        {
            // Retrieve correct answer from TempData
            CorrectBreed = TempData["CorrectBreed"] as string;

            // Check if answer is correct
            if (selectedBreed == CorrectBreed)
            {
                Score++;
                AnswerResult = "Correct! Great job!";
            }
            else
            {
                AnswerResult = $"Oops! The correct answer was: {CorrectBreed}.";
            }

            // Move to the next question
            CurrentQuestionNumber++;
            TempData["CurrentQuestionNumber"] = CurrentQuestionNumber;
            TempData["Score"] = Score;

            if (CurrentQuestionNumber > TotalQuestions)
            {
                QuizFinished = true;
                return Page();
            }

            // Load the next question
            await OnGetAsync();
            return Page();
        }
    }
}