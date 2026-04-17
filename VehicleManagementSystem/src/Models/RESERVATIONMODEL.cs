using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagementSystem.src.Models
{
    public class RESERVATIONMODEL
    {
        [Required]
        [Key]
        public int reservationid { get;set;}
        [Required]
        public int userid { get; set; }
        [ForeignKey("userid")]
        public USERMODEL user { get; set; }
        [Required]
        public int vehicleid { get; set; }
        [ForeignKey("vehicleid")]
        public VEHICLEMODEL vehicle { get; set; }
        public double price { get; set; }
        public DateTime reservedate { get; set; }
        public DateTime duedate { get; set; }
        public DateTime? returneddate { get; set; }
    }
}
