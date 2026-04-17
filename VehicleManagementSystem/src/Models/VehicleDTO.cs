using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.src.Models
{
    public class VehicleDTO
    {
        [Required(ErrorMessage = "Make is required")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "License plate is required")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? PriceRate { get; set; }

        [Required(ErrorMessage = "Image source url is required")]
        public string ImageURL { get; set; }
    }
}
