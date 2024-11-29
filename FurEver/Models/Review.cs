using System.ComponentModel.DataAnnotations;

namespace FurEver.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string? DogBreedName { get; set; }
        [StringLength(30)]
        public string? UserName { get; set; }
        public string? Comments { get; set; }
    }
}
