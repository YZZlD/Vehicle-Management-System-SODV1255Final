using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Models;

namespace VehicleManagementSystem.src.Data
{
    public class USERMODELDB:DbContext
    {
        //remove later
        public USERMODELDB(DbContextOptions<USERMODELDB> options) : base(options)
        {
        }
        public DbSet<USERMODEL> Users { get; set; }
    }
}
