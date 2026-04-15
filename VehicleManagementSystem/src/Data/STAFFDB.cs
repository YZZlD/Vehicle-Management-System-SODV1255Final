using Microsoft.EntityFrameworkCore;

namespace VehicleManagementSystem.src.Data
{
    public class STAFFDB:DbContext
    {
        public STAFFDB(DbContextOptions<STAFFDB> options) : base(options)
        {
        }
        public DbSet<Models.STAFFMODEL> Staffs { get; set; }
    }
}
