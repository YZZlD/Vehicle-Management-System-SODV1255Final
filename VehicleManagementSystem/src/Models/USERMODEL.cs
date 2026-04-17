using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.src.Models
{
    public class USERMODEL
    {
        [Required]
        [Key]
        public int userid { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
        public int age { get; set; }
        //public string billinginfo;
        //public double chargesappliedtoaccount;//Money?
        //public string Demographic;

    }
}
