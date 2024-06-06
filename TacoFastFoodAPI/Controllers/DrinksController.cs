using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();//get using statment--potential fixes
        [HttpGet()]
        //Returns IActionResult
        public IActionResult Drinks(string? name = null)
        {
            List<Drink> result = dbContext.Drinks.ToList();// call the staff
            if (name != null)//check to see if user provided a valid input
            {
                result = result.Where(b => b.Name == name).ToList();//this will return the filtered list if a valid input was given
            }
            return Ok(result);//Return status code and respons obj
        }
        //Custom Endpoint path
        [HttpGet("{id}")]
        //parameters added
        public IActionResult Drink(int id)
        {
            Drink result = dbContext.Drinks.Find(id);
            if (result == null)
            {
                return NotFound("Drink Id not found.");
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Drink([FromBody] Drink newDrink)
        {
            dbContext.Drinks.Add(newDrink);
            dbContext.SaveChanges();
            // The string is where they can find the object they just created
            //We link them back to the GetById method
            return Created($"/api/Taco/{newDrink.Id}", newDrink);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateDrink([FromBody] Drink targetDrink, int id)
        {
            if (id != targetDrink.Id) { return BadRequest(); }
            if (!dbContext.Drinks.Any(b => b.Id == id)) { return NotFound(); }
            dbContext.Drinks.Update(targetDrink);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
