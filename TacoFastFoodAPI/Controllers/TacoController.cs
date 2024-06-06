using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacoController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();//get using statment--potential fixes
        [HttpGet()]
        //Returns IActionResult
        public IActionResult Taco(string? name = null, string ApiKey) { 
            User request = UserDAL.ValidateKey(Apikey);
        
            List<Taco> result = dbContext.Tacos.ToList();// call the staff
            if (name != null)//check to see if user provided a valid input
            {
                result = result.Where(b => b.Name == name).ToList();//this will return the filtered list if a valid input was given
            }
            return Ok(result);//Return status code and respons obj
        }
        //Custom Endpoint path
        [HttpGet("{id}")]
        //parameters added
        public IActionResult Taco(int id)
        {
            Taco result = dbContext.Tacos.Find(id);
            if (result == null)
            {
                return NotFound("Taco Id not found.");
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Taco([FromBody] Taco newTaco)
        {
            dbContext.Tacos.Add(newTaco);
            dbContext.SaveChanges();
            // The string is where they can find the object they just created
            //We link them back to the GetById method
            return Created($"/api/Taco/{newTaco.Id}", newTaco);
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteTaco(int id)
        {
            Taco b = dbContext.Tacos.Find(id);
            if (b == null)
            {
                return NotFound();
            }
            dbContext.Tacos.Remove(b);
            dbContext.SaveChanges();
            //Common to return nothing when deleting
            return NoContent();
        }
    }
}
