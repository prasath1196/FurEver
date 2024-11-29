using FurEver.Data;
using FurEver.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FurEver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }

        [HttpGet("{breedName}")]
        public ActionResult<Review> GetById(string breedName)
        {
            var reviews = _context.Reviews
                                  .Where(r => r.DogBreedName.ToLower() == breedName.ToLower())
                                  .ToList();

            if (!reviews.Any())
            {
                return NotFound($"No reviews found for breed: {breedName}");
            }

            return Ok(reviews);
        }

        //// POST api/<ReviewsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ReviewsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ReviewsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
