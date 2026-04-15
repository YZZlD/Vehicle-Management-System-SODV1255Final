
using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Models;
namespace VehicleManagementSystem.src.Data
{
    public class VEHICLEMODELDB:DbContext
    {
        public VEHICLEMODELDB(DbContextOptions<VEHICLEMODELDB> options) : base(options)
        {
        }
        public DbSet<VEHICLEMODEL> Vehicles { get; set; }
    }
}
