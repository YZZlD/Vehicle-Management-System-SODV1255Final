using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.src.Models
{
    public class STAFFMODEL
    {
        [Required]
        public int staffid;
        [Required]
        public string password;
        [Required]
        public string username; 
    }
}
