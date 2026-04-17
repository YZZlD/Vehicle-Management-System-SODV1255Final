using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Data;
using VehicleManagementSystem.src.Models;

namespace VehicleManagementSystem.src.Repositories
{
    public class VEHICLEREPO
    {
        private readonly APPCONTEXTDB appdb;

        public VEHICLEREPO(APPCONTEXTDB context)
        {
            appdb = context;
        }

        public async Task<List<VEHICLEMODEL>> Index()
        {
            return await appdb.vehiclemodel.ToListAsync();
        }
        public async Task<VEHICLEMODEL?> Getbyid(int id)
        {
            return await appdb.vehiclemodel.FirstOrDefaultAsync(u => u.vehicleid == id);
        }
        public async Task AddVehicle(VEHICLEMODEL newvehicle)
        {
            await appdb.vehiclemodel.AddAsync(newvehicle);
            await appdb.SaveChangesAsync();
        }
        public async Task EditVehicle(VEHICLEMODEL newvehicle)
        {
            VEHICLEMODEL vehiclecheck = await appdb.vehiclemodel.Where(u => u.vehicleid == newvehicle.vehicleid).FirstOrDefaultAsync();
            if (vehiclecheck != null)
            {
                Console.WriteLine("No vehicle found"); return;
            }
            await appdb.vehiclemodel.Where(u => u.vehicleid == newvehicle.vehicleid).ExecuteUpdateAsync(u => u
            .SetProperty(u => u.licenseplate, newvehicle.licenseplate)
            .SetProperty(u=> u.model, newvehicle.model)
            .SetProperty(u=>u.make, newvehicle.make)
            );
            await appdb.SaveChangesAsync();
        }
        public async Task DeleteVehicle(int id)
        {
            await appdb.vehiclemodel.Where(u=> u.vehicleid == id).ExecuteDeleteAsync();
            await appdb.SaveChangesAsync();
        }
    }
}
