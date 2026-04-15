using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.src.Models
{
    public class RESERVATIONMODEL
    {
        [Required]
        public int reservationid;
        public int userid;//usermade reservation
        public int vehicleid;
        public double price;
        public string locationrented;
        public double appliedtaxes;
        public string[] nameofappliedtaxes;//maybe put down and keep track what taxes applied to a reservation? remove later if not
        public DateTime reservedate;
        public DateTime duedate;
        public DateTime returneddate;
    }
}
