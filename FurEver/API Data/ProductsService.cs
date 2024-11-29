using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using FurEver.Models;
using FurEver.Pages;

namespace FurEver.API_Data
{
    public class ProductsService
    {
        public static ProductsResponse FromJson(string json) => JsonConvert.DeserializeObject<ProductsResponse>(json);

        public static async Task<List<Product>> FetchProductsAsync()
        {

            var constants = Constant.Instace;
            var productsAPI = constants.ProductsAPI;
            var apiClient = new HttpClient();
            try
            {
                HttpResponseMessage response = await apiClient.GetAsync(productsAPI);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    ProductsResponse productsResponse = FromJson(jsonResponse); 

                    if (productsResponse?.Data != null && productsResponse?.Data.totalRecords != "0")
                    {
                        return productsResponse.Data.Products;
                    }
                    else
                    {
                        return new List<Product>();
                    }
                }
                else
                {
                    ;
                    return new List<Product>(); // Return an empty list on failure
                }
            }
            catch (Exception ex)
            {
                return new List<Product>();
            }
        }

    }

    public class ProductsResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("payload")]
        public Payload? Data { get; set; }
    }

    public class Payload
    {
        [JsonProperty("totalRecords")]
        public string totalRecords { get; set; }

        [JsonProperty("records")]
        public List<Product>? Products { get; set; }
    }

    public class Product
    {

        [JsonProperty("productId")]
        public string productId { get; set; }

        [JsonProperty("productType")]
        public string productType { get; set; }

        [JsonProperty("descriptors")]
        public Descriptors Descriptors { get; set; }

        [JsonProperty("skus")]

        public List<Sku> ProductDetails { get; set; }

        [JsonProperty("searchAndSeo")]
        public Redirect RedirectDetails { get; set; }

    }

    public class Descriptors
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("productdescription")]
        public string ProductDescription { get; set; }
    }

    public class Sku
    {
        [JsonProperty("skuId")]
        public string SkuId { get; set; }

        [JsonProperty("onlineOffer")]
        public OnlineOffer OnlineOffer { get; set; }

        [JsonProperty("assets")]
        public Asset asset { get; set; }
    }   

    public class OnlineOffer
    {
        [JsonProperty("price")]
        public Price Price { get; set; }
    }

    public class Price
    {
        [JsonProperty("finalPrice")]
        public FinalPrice FinalPrice { get; set; }
    }

    public class FinalPrice
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }


    public class Asset
    {
        [JsonProperty("image")]
        public string ImageID { get; set; }
    }

    public class Redirect
    {
        [JsonProperty("url")]
        public string RedirectUrl { get; set; }
    }
}
