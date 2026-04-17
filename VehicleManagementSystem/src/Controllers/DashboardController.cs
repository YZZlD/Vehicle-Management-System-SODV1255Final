using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.src.Repositories;
using VehicleManagementSystem.ViewModels;

namespace VehicleManagementSystem.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly USERREPO _customerRepository;
        private readonly VEHICLEREPO _vehicleRepository;
        private readonly RESERVEREPO _reservationRepository;

        public DashboardController(USERREPO customerRepository, VEHICLEREPO vehicleRepository, RESERVEREPO reservationRepository)
        {
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleRepository.Index();
            var customers = await _customerRepository.GetAllUsers();
            var reservations = await _reservationRepository.index();

            var now = DateTime.UtcNow;

            var model = new DashboardViewModel
            {
                VehicleCount = vehicles.Count(),
                CustomerCount = customers.Count(),
                ReservationCount = reservations.Count(),

                Vehicles = vehicles.Take(3).ToList(),
                Customers = customers.Take(3).ToList(),
                Reservations = reservations.Take(3).ToList(),

                UpcomingRentals = reservations
                    .Where(reservation => reservation.duedate > now && reservation.duedate <= now.AddDays(7))
                    .OrderBy(reservation => reservation.duedate)
                    .ToList(),

                OverdueRentals = reservations
                    .Where(reservation => reservation.duedate < now)
                    .Where(reservation => reservation.returneddate == null)
                    .OrderBy(reservation => reservation.duedate)
                    .ToList()
            };

            return View(model);
        }
    }
}
