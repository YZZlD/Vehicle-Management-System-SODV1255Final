using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.src.Models
{
    public class CustomerDTO
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Range(1, 120, ErrorMessage = "Age must be valid")]
        public int Age { get; set; }
    }
}
