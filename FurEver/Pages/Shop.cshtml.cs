using FurEver.API_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurEver.Pages
{
    public class ShopModel : PageModel
    {
          
        [BindProperty(SupportsGet = true)]
        public List<API_Data.Product>? ProductList{ get; set; }

        public async Task OnGetAsync()
        {
            ProductList = await ProductsService.FetchProductsAsync();
            Console.WriteLine(ProductList);
        }
        
    }
}
