using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.src.Models
{
    public class USERMODEL
    {
        [Required]
        public int userid;
        [Required]
        public string username;
        [Required]
        public string password;
        public string fname;
        public string lname;
        public string billinginfo;
        public double chargesappliedtoaccount;//Money?
        public string Demographic;
        
    }
}
