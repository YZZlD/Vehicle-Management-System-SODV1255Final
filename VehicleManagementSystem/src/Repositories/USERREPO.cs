using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.src.Data;
using VehicleManagementSystem.src.Models;
namespace VehicleManagementSystem.src.Repositories
{//USERREPO IS CUSTOMER
    public class USERREPO
    {
        //public readonly USERMODELDB userdb;
        private readonly APPCONTEXTDB appcontextdb;

        public USERREPO(APPCONTEXTDB context)
        {
            appcontextdb = context;
        }

        public async Task<List<Models.USERMODEL>> GetAllUsers()
        {
            //GET LIST
            return await appcontextdb.usermodel.ToListAsync();
        }
        public async Task AddCustomer(Models.USERMODEL newuser)
        {
            //GET BY ID
            await appcontextdb.usermodel.AddAsync(newuser);
            await appcontextdb.SaveChangesAsync();
            Console.WriteLine("User added successfully");
        }

        public async Task Edit(USERMODEL updatedUser) //Update
        {
            USERMODEL usertobeedited = await appcontextdb.usermodel.FindAsync(updatedUser.userid);
            if (usertobeedited == null) { Console.WriteLine("USER NOT FOUND"); return; }
            await appcontextdb.usermodel.Where(u => u.userid == updatedUser.userid).ExecuteUpdateAsync(u => u
            .SetProperty(u => u.fname, updatedUser.fname)
            .SetProperty(u => u.lname, updatedUser.lname)
            .SetProperty(u => u.email, updatedUser.email)
            .SetProperty(u => u.phonenumber, updatedUser.phonenumber)
            .SetProperty(u => u.age, updatedUser.age)
            );

        await appcontextdb.SaveChangesAsync();
        }
        public async Task<USERMODEL?> getuserbyid(int id)
        {
            return await appcontextdb.usermodel.FirstOrDefaultAsync( u => u.userid == id);
        }

        public async Task Deletebyid(int id)
        {
            await appcontextdb.usermodel.Where(u=> u.userid == id).ExecuteDeleteAsync();
            await appcontextdb.SaveChangesAsync();
        }
    }
}
