using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Data;
using VehicleManagementSystem.src.Models;
namespace VehicleManagementSystem.src.Repositories
{
    public class STAFFREPO
    {
        //public readonly STAFFDB staffdb;
        private readonly APPCONTEXTDB appcontextdb;

        public async Task<List<Models.STAFFMODEL>> GetAllStaffs()
        {
            //GET LIST
            return await appcontextdb.staffmodel.ToListAsync();
        }

        public async Task<Models.STAFFMODEL> GetStaffById(int id)
        {
            //GET BY ID
            return await appcontextdb.staffmodel.FirstOrDefaultAsync(u => u.staffid == id);
        }
        public async void AddUser(STAFFMODEL newstaff)
        {
            await appcontextdb.staffmodel.AddAsync(newstaff);
            await appcontextdb.SaveChangesAsync();
        }
    }
}
