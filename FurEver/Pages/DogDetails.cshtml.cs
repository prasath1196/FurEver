using FurEver.API_Data;
using FurEver.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace FurEver.Pages
{
    public class DogDetailsModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string BreedName { get; set; }

        [BindProperty(SupportsGet = true)]
        public DogBreed? DogDetails{ get; set; }
        public async Task  OnGetAsync()
        {
            var appConstants = Constant.Instace;
            var httpClient = new HttpClient();
            var allDogBreeds = await DogBreedService.FetchBreedsAsync();
            DogDetails = allDogBreeds.Where(row => row?.General?.Name == BreedName).FirstOrDefault();


        } 
    }
}
