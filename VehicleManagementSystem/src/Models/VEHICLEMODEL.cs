using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.src.Models
{
    public class VEHICLEMODEL
    {
        [Required]
        [Key]
        public int vehicleid { get; set; }
        public string licenseplate { get; set; }
        public string model { get; set; }
        public string make { get; set; }
        public double price { get; set; }
        public string imagelinkplaintext { get; set; }
    }
}
