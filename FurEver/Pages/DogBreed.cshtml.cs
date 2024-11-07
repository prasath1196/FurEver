using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FurEver.Pages
{
    public class DogBreedModel : PageModel
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<DogBreedModel> _logger;
        public List<DogBreedData> DogBreeds { get; set; } = new List<DogBreedData>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } // Property to hold the search term
        public DogBreedModel(IHttpClientFactory httpClientFactory, ILogger<DogBreedModel> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            string apiUrl = "https://registry.dog/api/v1";
            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);

                // Console.WriteLine("API Response: " + response);
                _logger.LogInformation("API Response: {Response}", response);

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);

                DogBreeds = apiResponse.Data;
                //DogBreeds = JsonConvert.DeserializeObject<List<DogBreed>>(response);

                // Filter the results based on the search term
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
                //Console.WriteLine("Error fetching data: " + ex.Message);

                _logger.LogError(ex, "Error fetching data");

                DogBreeds = new List<DogBreedData>();
            }
        }
    }

        public class DogBreed
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("popularity")]
            public string Popularity { get; set; }


        }

    public class ApiResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<DogBreedData> Data { get; set; }
    }

    public class DogBreedData
    {
        [JsonProperty("general")]
        public GeneralInfo General { get; set; }

        [JsonProperty("physical")]
        public PhysicalInfo Physical { get; set; }

        [JsonProperty("behavior")]
        public BehaviorInfo Behavior { get; set; }

        [JsonProperty("care")]
        public CareInfo Care { get; set; }

        [JsonProperty("images")]
        public ImagesInfo Images { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class GeneralInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("personalityTraits")]
        public List<string> PersonalityTraits { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("longDescription")]
        public string LongDescription { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("lifespan")]
        public int Lifespan { get; set; }
    }

    public class PhysicalInfo
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("lifespan")]
        public int Lifespan { get; set; }

        [JsonProperty("droolingFrequency")]
        public int DroolingFrequency { get; set; }

        [JsonProperty("coatStyle")]
        public string CoatStyle { get; set; }

        [JsonProperty("coatTexture")]
        public string CoatTexture { get; set; }

        [JsonProperty("coatLength")]
        public int CoatLength { get; set; }

        [JsonProperty("doubleCoat")]
        public bool DoubleCoat { get; set; }
    }

    public class BehaviorInfo
    {
        [JsonProperty("familyAffection")]
        public int FamilyAffection { get; set; }

        [JsonProperty("childFriendly")]
        public int ChildFriendly { get; set; }

        [JsonProperty("dogSociability")]
        public int DogSociability { get; set; }

        [JsonProperty("friendlinessToStrangers")]
        public int FriendlinessToStrangers { get; set; }

        [JsonProperty("playfulness")]
        public int Playfulness { get; set; }

        [JsonProperty("protectiveInstincts")]
        public int ProtectiveInstincts { get; set; }

        [JsonProperty("adaptability")]
        public int Adaptability { get; set; }

        [JsonProperty("barkingFrequency")]
        public int BarkingFrequency { get; set; }
    }

    public class CareInfo
    {
        [JsonProperty("sheddingAmount")]
        public int SheddingAmount { get; set; }

        [JsonProperty("groomingFrequency")]
        public int GroomingFrequency { get; set; }

        [JsonProperty("exerciseNeeds")]
        public int ExerciseNeeds { get; set; }

        [JsonProperty("mentalStimulationNeeds")]
        public int MentalStimulationNeeds { get; set; }

        [JsonProperty("trainingDifficulty")]
        public int TrainingDifficulty { get; set; }
    }

    public class ImagesInfo
    {
        [JsonProperty("small")]
        public ImageSet Small { get; set; }

        [JsonProperty("large")]
        public ImageSet Large { get; set; }
    }

    public class ImageSet
    {
        [JsonProperty("indoors")]
        public string Indoors { get; set; }

        [JsonProperty("outdoors")]
        public string Outdoors { get; set; }

        [JsonProperty("studio")]
        public string Studio { get; set; }
    }

}
