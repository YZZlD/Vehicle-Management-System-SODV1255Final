using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.src.Repositories;
using VehicleManagementSystem.ViewModels;

namespace VehicleManagementSystem.Controllers
{
    public class ReportController : BaseController
    {
        private readonly RESERVEREPO _reservationRepository;

        public ReportController(RESERVEREPO reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        //Filtering is handled through regetting the route with new parameter information internally from the generate button through the view.
        //With empty parameters all reservations will be included.
        public async Task<IActionResult> Index(string dateFrom, string dateTo, string vehicleMake, int? minAge, int? maxAge)
        {
            var reservations = await _reservationRepository.index();

            //We need to grab a list of makes to show in the filtering form
            var makes = reservations
                            .Select(reservation => reservation.vehicle.make)
                            .Distinct()
                            .ToList();

            //Filtering here empty input using DATETIME defaults
            DateTime from = string.IsNullOrWhiteSpace(dateFrom) ? DateTime.MinValue : DateTime.Parse(dateFrom);
            DateTime to = string.IsNullOrWhiteSpace(dateTo) ? DateTime.MaxValue : DateTime.Parse(dateTo);

            //These are all validation for empty inputs and if empty we simply do not apply the filtering logic
            var filteredReservations = reservations.Where(reservation => reservation.reservedate >= from && reservation.reservedate <= to);

            if(!string.IsNullOrWhiteSpace(vehicleMake))
            {
                filteredReservations = filteredReservations.Where(reservation => reservation.vehicle.make == vehicleMake);
            }

            if(minAge.HasValue)
            {
                filteredReservations = filteredReservations.Where(reservation => reservation.user.age >= minAge.Value);
            }

            if(maxAge.HasValue)
            {
                filteredReservations = filteredReservations.Where(reservation => reservation.user.age <= maxAge.Value);
            }

            //Filter information from the filteredReservations object to use in the report view model
            var totalRevenue = filteredReservations.Sum(reservation => reservation.price);
            var totalReservations = filteredReservations.Count();
            var activeRentals = filteredReservations.Where(reservation => DateTime.Today >= reservation.reservedate && DateTime.Today <= reservation.duedate).ToList().Count();
            var mostPopularVehicle = filteredReservations.GroupBy(reservation => reservation.vehicle.model)
                                        .OrderByDescending(group => group.Count())
                                        .Select(group => group.Key)
                                        .FirstOrDefault();

            var reportViewModel = new ReportViewModel
            {
                Reservations = filteredReservations.ToList(),
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
