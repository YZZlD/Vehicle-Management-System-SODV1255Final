using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.src.Models
{
    public class STAFFMODEL
    {
        [Required]
        [Key]
        public int staffid { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string username { get; set; }
    }
}
