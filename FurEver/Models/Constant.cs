namespace FurEver.Models
{
    public class Constant
    {
        public static readonly Constant Instace = new Constant();
        public string DogBReedsAPI { get; } = "https://registry.dog/api/v1";

        public string ProductsAPI { get; } = "https://dev-y8a2nqb37bs9abs.api.raw-labs.com/sams_club/dog_products";

        public string ProductRedirectRoot { get; } = "https://www.samsclub.com";

        public string ProductsImageRootURL { get; } = "https://scene7.samsclub.com/is/image/samsclub/";
    }
}
