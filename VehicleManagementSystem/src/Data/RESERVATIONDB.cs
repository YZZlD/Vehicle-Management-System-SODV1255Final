using VehicleManagementSystem.src.Models;
using Microsoft.EntityFrameworkCore;

namespace VehicleManagementSystem.src.Data
{
    public class RESERVATIONDB: DbContext
    {
        public RESERVATIONDB(DbContextOptions<RESERVATIONDB> options) : base(options)
        {
        }
        public DbSet<RESERVATIONMODEL> Reservations { get; set; }
    }
}
