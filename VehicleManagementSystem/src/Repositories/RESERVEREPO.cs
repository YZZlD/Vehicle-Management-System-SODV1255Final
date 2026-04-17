using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleManagementSystem.src.Data;
using VehicleManagementSystem.src.Models;
namespace VehicleManagementSystem.src.Repositories
{
    public class RESERVEREPO
    {
        private readonly APPCONTEXTDB appdb;

        public async Task<List<RESERVATIONMODEL>> index()
        {
            return await appdb.reservationmodel.Include(b=>b.vehicle).Include(b=>b.user).ToListAsync();
        }
        public async void Addreserve(RESERVATIONMODEL newreservation)
        {
            await appdb.reservationmodel.AddAsync(newreservation);
            await appdb.SaveChangesAsync();
        }
        public async Task<RESERVATIONMODEL?> getbyid(int id)
        {
            return await appdb.reservationmodel.Include(b => b.vehicle).Include(b => b.userid).FirstOrDefaultAsync(u => u.reservationid == id);
        }
        public async void Deletebyid(int id)
        {
            await appdb.reservationmodel.Where(u => u.reservationid == id).ExecuteDeleteAsync();
            await appdb.SaveChangesAsync();
        }
        public async void Edit(RESERVATIONMODEL updatedreservation)
        {
            RESERVATIONMODEL RESERVECHECK = await appdb.reservationmodel.FirstOrDefaultAsync(u => u.reservationid == updatedreservation.reservationid);
            if (RESERVECHECK == null)
            {
                Console.WriteLine("No Reservation found");
                return;
            }
            await appdb.reservationmodel.Where(u => u.reservationid == updatedreservation.reservationid).ExecuteUpdateAsync(
                u => u
                .SetProperty(u => u.reservedate, updatedreservation.reservedate)
                .SetProperty(u => u.userid, updatedreservation.userid)
                .SetProperty(u => u.vehicleid, updatedreservation.vehicleid)
                .SetProperty(u => u.price, updatedreservation.price)
                .SetProperty(u => u.reservedate, updatedreservation.reservedate)
                .SetProperty(u => u.duedate, updatedreservation.duedate)
                .SetProperty(u => u.returneddate, updatedreservation.returneddate)
                );
        }

        public async void Delete(int id)
        {
            await appdb.reservationmodel.Where(u=>u.reservationid == id).ExecuteDeleteAsync();
            await appdb.SaveChangesAsync();
        }
    }
}

//[Required]
//[Key]
//public int reservationid { get; set; }
//[Required]
//public int userid { get; set; }
//[ForeignKey("userid")]
//public USERMODEL user { get; set; }
//[Required]
//public int vehicleid { get; set; }
//[ForeignKey("vehicleid")]
//public VEHICLEMODEL vehicle { get; set; }
//public double price { get; set; }
//public DateTime reservedate { get; set; }
//public DateTime duedate { get; set; }
//public DateTime? returneddate { get; set; }
