using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using FurEver.Models;

namespace FurEver.API_Data
{
    public class DogBreedService
    {
        // Static Methods
        public static DogBreedsResponse FromJson(string json) => JsonConvert.DeserializeObject<DogBreedsResponse>(json);

        public static async Task<List<DogBreed>> FetchBreedsAsync()
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

                    // Check if data exists and if 'general' is not null
                    if (dogBreedsResponse?.Data != null && dogBreedsResponse?.Data.Count > 0)
                    {
                        Console.WriteLine("Successfully fetched dog breeds.");
                        return dogBreedsResponse.Data; // Return the full list of DogBreed objects
                    }
                    else
                    {
                        Console.WriteLine("No dog breeds found.");
                        return new List<DogBreed>(); // Return an empty list if no breeds found
                    }
                }
                else
                {
                    // Handle the error if the API response is not successful
                    Console.WriteLine($"Error fetching dog breeds: {response.StatusCode}");
                    return new List<DogBreed>(); // Return an empty list on failure
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions during the API call
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

    // DogBreed model should not have a Name property directly
    public class DogBreed
    {
        // General Information (mapped from 'general' key in JSON)
        [JsonProperty("general")]
        public GeneralData? General { get; set; }

        // Images (mapped from 'images' key in JSON)
        [JsonProperty("images")]
        public DogBreedImages? Images { get; set; }
    }

    // This holds the breed's general data (name, group, etc.)
    public class GeneralData
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("group")]
        public string? Group { get; set; }
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
