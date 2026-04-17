using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Models;
namespace VehicleManagementSystem.src.Data
{
    public class APPCONTEXTDB:DbContext
    {
        public APPCONTEXTDB(DbContextOptions<APPCONTEXTDB> options) : base(options)
        {

        }
        public DbSet<USERMODEL> usermodel { get; set; }
        public DbSet<STAFFMODEL> staffmodel { get; set; }
        public DbSet<VEHICLEMODEL> vehiclemodel { get; set; }
        public DbSet<RESERVATIONMODEL> reservationmodel { get; set; }
    }
}
