using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using FurEver.Models;
using FurEver.Pages;

namespace FurEver.API_Data
{
    // Summary
    // DogBreed Service Class handles the API Call to the external data source and returns the result
    // ReturnType: DogBreed
    public class DogBreedService
    {
        // Mapping the API Response to DogBreedsResponse Class
        public static DogBreedsResponse FromJson(string json) => JsonConvert.DeserializeObject<DogBreedsResponse>(json);

        public static async Task<List<DogBreed>> FetchBreedsAsync(string searchTerm = "")
        {
            var constants = Constant.Instace;
            var dogBreedsAPI = constants.DogBReedsAPI;

            var apiClient = new HttpClient();
            try
            {
                HttpResponseMessage response = await apiClient.GetAsync(dogBreedsAPI);
                Console.WriteLine(response);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    DogBreedsResponse dogBreedsResponse = FromJson(jsonResponse); // Deserialize into DogBreedsResponse object

                    if (dogBreedsResponse?.Data != null && dogBreedsResponse?.Data.Count > 0)
                    {
                        // Console.WriteLine("Successfully fetched dog breeds.");
                        //return dogBreedsResponse.Data;

                        // If a search term is provided, filter the dog breeds by name
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            return dogBreedsResponse.Data
                                .Where(breed => breed.General.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                                .ToList();
                        }
                        else
                        {
                            return dogBreedsResponse.Data; // Return all breeds if no search term is given
                        }
                    }
                    else
                    {
                        Console.WriteLine("No dog breeds found.");
                        return new List<DogBreed>();
                    }
                }
                else
                {
                    Console.WriteLine($"Error fetching dog breeds: {response.StatusCode}");
                    return new List<DogBreed>(); // Return an empty list on failure
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<DogBreed>(); // Return an empty list on error
            }
        }
    }

    // Response model to reflect the outer response structure
    public class DogBreedsResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<DogBreed>? Data { get; set; }
    }

    // Model for the DogBreed Class. Contains the Details for each Dog Breed
    public class DogBreed
    {
        // General Information (mapped from 'general' key in JSON)
        [JsonProperty("general")]
        public GeneralData? General { get; set; }

        [JsonProperty("physical")]
        public PhysicalInfo Physical { get; set; }

        [JsonProperty("behavior")]
        public BehaviorInfo? Behavior { get; set; }

        [JsonProperty("care")]
        public CareInfo? Care { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }

        // Images (mapped from 'images' key in JSON)
        [JsonProperty("images")]
        public DogBreedImages? Images { get; set; }
    }

    // This holds the breed's general data (name, group, etc.)
    public class GeneralData
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
        public string Id { get; internal set; }
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


    // Images (small/large)
    public class DogBreedImages
    {
        [JsonProperty("small")]
        public DogBreedImage? Small { get; set; }

        [JsonProperty("large")]
        public DogBreedImage? Large { get; set; }
    }

    // Individual image information (indoors, outdoors, studio)
    public class DogBreedImage
    {
        [JsonProperty("indoors")]
        public string? Indoors { get; set; }

        [JsonProperty("outdoors")]
        public string? Outdoors { get; set; }

        [JsonProperty("studio")]
        public string? Studio { get; set; }
    }
}
