using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace VehicleManagementSystem.Controllers
{
    public class ReportController : Controller
    {
        // private readonly ReservationRepository _reservationRepository;

        // public ReportController(ReservationRepository reservationRepository)
        // {
        //     _reservationRepository = reservationRepository;
        // }

        //Filtering is handled through regetting the route with new parameter information internally from the generate button through the view.
        //With empty parameters all reservations will be included.
        public async Task<IActionResult> Index(string dateFrom, string dateTo, string vehicles, string customers)
        {
            var reservations = _reservationRepository.GetAllReservations();

            DateTime lowerboundDate = string.IsNullOrEmpty(dateFrom.Trim()) ? DateTime.Parse("01-01-2000") : DateTime.Parse(dateFrom);
            DateTime upperboundDate = string.IsNullOrEmpty(dateTo.Trim()) ? DateTime.Today : DateTime.Parse(dateTo);

            List<string> vehicleList = vehicles.Split(',').ToList();
            List<string> customerList = customers.Split(',').ToList();

            //Basic first filtering implementation (UNTESTED)
            var filteredReservations = reservations
                                        .Where(r => r.DateTime >= lowerboundDate && r.DateTime <= upperboundDate)
                                        .Where(r => vehicleList.Contains(r.vehicle))
                                        .Where(r => vehicleList.Contains(r.vehicle))
                                        .ToList();

            //Basic logic grabs for different necessary information for the view (UNTESTED)
            var totalRevenue = filteredReservations.Sum(r => r.revenue);
            var totalReservations = filteredReservations.Count();
            var activeRentals = filteredReservations.Where(r => DateTime.Today >= r.ReservedDate && DateTime.Today <= r.DueDate).ToList().Count();
            var mostCommonVehicle = filteredReservations.GroupBy(r => r.Vehicle)
                                        .OrderByDescending(group => group.Count)
                                        .Select(group => group.Key)
                                        .First();

            return View(filteredReservations, totalRevenue, totalReservations, activeRentals, mostCommonVehicle);
        }
    }
}
