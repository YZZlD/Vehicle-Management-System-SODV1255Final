using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Models;

namespace VehicleManagementSystem.src.Data
{
    public class USERMODELDB:DbContext
    {
        public USERMODELDB(DbContextOptions<USERMODELDB> options) : base(options)
        {
        }
        public DbSet<USERMODEL> Users { get; set; }
    }
}
