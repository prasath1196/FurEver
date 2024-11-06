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
        public string CorrectBreed { get; set; }
        public string AnswerResult { get; set; }
        public int Score { get; set; }
        public int CurrentQuestionNumber { get; set; } = 1;
        public int TotalQuestions { get; set; } = 5;
        public bool QuizFinished { get; set; } = false;
        public List<string> QuizImages { get; set; } = new List<string>();

        private static readonly string[] DogBreeds =
        {
        "beagle", "bulldog", "dalmatian", "golden retriever", "labrador", "poodle", "pug", "shiba", "husky", "german shepherd"
    };

        public async Task OnGetAsync()
        {
            if (CurrentQuestionNumber > TotalQuestions)
            {
                QuizFinished = true;
                return;
            }

            if (!QuizImages.Any())
            {
                // Load 5 unique images
                using (var httpClient = new HttpClient())
                {
                    for (int i = 0; i < TotalQuestions; i++)
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
            if (selectedBreed == CorrectBreed)
            {
                Score++;
                AnswerResult = "Correct! Great job!";
            }
            else
            {
                AnswerResult = $"Oops! The correct answer was: {CorrectBreed}.";
            }

            CurrentQuestionNumber++;
            if (CurrentQuestionNumber > TotalQuestions)
            {
                QuizFinished = true;
                return Page();
            }

            // Call OnGetAsync to load the next question
            await OnGetAsync();
            return Page();
        }
    }
}