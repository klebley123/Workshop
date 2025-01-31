using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopWebAPI.Data;
using WorkshopWebApi.Models;

namespace WorkshopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOrdersController : ControllerBase
    {
        private readonly WorkshopContext _context;

        public ServiceOrdersController(WorkshopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceOrder>>> GetServiceOrders()
        {
            return await _context.ServiceOrders.Include(o => o.Car).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ServiceOrder>> PostServiceOrder(ServiceOrder serviceOrder)
        {
            _context.ServiceOrders.Add(serviceOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceOrders), new { id = serviceOrder.Id }, serviceOrder);
        }
    }
}
