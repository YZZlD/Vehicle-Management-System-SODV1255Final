using VehicleManagementSystem.src.Models;
using VehicleManagementSystem.src.Repositories;

namespace VehicleManagementSystem.ViewModels
{
    public class DashboardViewModel
    {
        public List<VEHICLEMODEL> Vehicles { get; set; }
        public List<USERMODEL> Customers { get; set; }
        public List<RESERVATIONMODEL> Reservations { get; set; }

        public List<RESERVATIONMODEL> UpcomingRentals { get; set; }
        public List<RESERVATIONMODEL> OverdueRentals { get; set; }
    }
}
