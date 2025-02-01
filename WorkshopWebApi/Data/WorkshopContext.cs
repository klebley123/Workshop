using Microsoft.EntityFrameworkCore;
using WorkshopWebApi.Models;

namespace WorkshopWebAPI.Data
{
    public class WorkshopContext : DbContext
    {
        public WorkshopContext(DbContextOptions<WorkshopContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
    }
}
