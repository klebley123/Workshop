using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopWebAPI.Data;
using WorkshopWebApi.Models;

namespace WorkshopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly WorkshopContext _context;

        public CarsController(WorkshopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.Where(c => !c.IsDeleted).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCars), new { id = car.Id }, car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            car.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    
    }
}
