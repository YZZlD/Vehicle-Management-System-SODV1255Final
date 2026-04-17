using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace VehicleManagementSystem.Controllers
{
    public class ReportController : BaseController
    {
        // private readonly ReservationRepository _reservationRepository;

        // public ReportController(ReservationRepository reservationRepository)
        // {
        //     _reservationRepository = reservationRepository;
        // }

        //Filtering is handled through regetting the route with new parameter information internally from the generate button through the view.
        //With empty parameters all reservations will be included.
        public async Task<IActionResult> Index(string dateFrom, string dateTo, string vehicleMake, int? minAge, int? maxAge)
        {
            //THIS MAY BREAK WITH REPOSITORY INTRODUCTION SO IT WILL BE FIXED THEN

            var reservations = await _reservationRepository.GetAllReservations().Include(reservation => reservation.Vehicle).Include(reservation => reservation.Customer);

            var makes = await _reservationRepository
                                .GetAllReservations()
                                .Include(reservation => reservation.Vehicle)
                                .Select(reservation => reservation.Vehicle.Make)
                                .Distinct()
                                .ToList();

            DateTime from = string.IsNullOrWhiteSpace(dateFrom) ? DateTime.MinValue : DateTime.Parse(dateFrom);
            DateTime to = string.IsNullOrWhiteSpace(dateTo) ? DateTime.MaxValue : DateTime.Parse(dateTo);

            var filteredReservations = reservations.Where(reservation => reservation.ReservedDate >= from && reservation.ReservedDate <= to);

            if(!string.IsNullOrWhiteSpace(vehicleMake))
            {
                filteredReservations = filteredReservations.Where(reservation => reservation.Vehicle.Make == vehicleMake);
            }

            if(minAge.HasValue)
            {
                filteredReservations = filteredReservations.Where(reservation => reservation.Customer.Age >= minAge.Value);
            }

            if(maxAge.HasValue)
            {
                filteredReservations = filteredReservations.Where(reservation => reservation.Customer.Age <= maxAge.Value);
            }

            //Basic first filtering implementation (UNTESTED)
            //Logic error here as it will return nothing if either vehicle or customer are empty.
            // var filteredReservations = reservations
            //                             .Where(r => r.DateTime >= from && r.DateTime <= to)
            //                             .Where(r => vehicleList.Contains(r.Vehicle))
            //                             .Where(r => customerList.Contains(r.Customer))
            //                             .ToList();

            //Basic logic grabs for different necessary information for the view (UNTESTED)
            var totalRevenue = filteredReservations.Sum(reservation => reservation.TotalPrice);
            var totalReservations = filteredReservations.Count();
            var activeRentals = filteredReservations.Where(reservation => DateTime.Today >= reservation.ReservedDate && DateTime.Today <= reservation.DueDate).ToList().Count();
            var mostPopularVehicle = filteredReservations.GroupBy(reservation => reservation.Vehicle.Model)
                                        .OrderByDescending(group => group.Count)
                                        .Select(group => group.Key)
                                        .FirstOrDefault();

            var reportViewModel = new ReportViewModel
            {
                Reservations = filteredReservations,
                TotalRevenue = totalRevenue,
                TotalReservations = totalReservations,
                ActiveRentals = activeRentals,
                MostPopularVehicle = mostPopularVehicle,
                Makes = makes
            };

            return View(reportViewModel);
        }
    }
}
