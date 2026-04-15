using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.src.Models
{
    public class VEHICLEMODEL
    {
        [Required]
        public int vehicleid;
        public string licenseplate;
        public string model;
    }
}
