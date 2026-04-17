using VehicleManagementSystem.src.Models;

namespace VehicleManagementSystem.ViewModels
{
    public class ReportViewModel
    {
        public List<RESERVATIONMODEL> Reservations { get; set; }

        public double TotalRevenue { get; set; }

        public int TotalReservations { get; set; }

        public int ActiveRentals { get; set; }

        public string MostPopularVehicle { get; set; }

        public List<string> Makes { get; set; }
    }
}
