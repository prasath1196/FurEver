using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using FurEver.Models;

namespace FurEver.API_Data
{
    // Summary
    // DogBreed Service Class handles the API Call to the external data source and returns the result
    // ReturnType: DogBreed
    public class DogBreedService
    {
        // Mapping the API Response to DogBreedsResponse Class
        public static DogBreedsResponse FromJson(string json) => JsonConvert.DeserializeObject<DogBreedsResponse>(json);

        public static async Task<List<DogBreed>> FetchBreedsAsync()
        {
            var config = Constant.Instace;
            var dogBreedsAPI = config.DogBReedsAPI;

            var apiClient = new HttpClient();
            try
            {
                HttpResponseMessage response = await apiClient.GetAsync(dogBreedsAPI);
                Console.WriteLine($"API Response Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    DogBreedsResponse dogBreedsResponse = FromJson(jsonResponse); // Deserialize into DogBreedsResponse object

                    if (dogBreedsResponse?.Data != null && dogBreedsResponse?.Data.Count > 0)
                    {
                        Console.WriteLine("Successfully fetched dog breeds.");
                        return dogBreedsResponse.Data;
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


        [JsonProperty("shortDescription")]
        public string? ShortDescription { get; set; }

        [JsonProperty("longDescription")]
        public string? LongDescription { get; set; }
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
