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
                //Grab counts to display and tie them to the viewModel property
                VehicleCount = vehicles.Count(),
                CustomerCount = customers.Count(),
                ReservationCount = reservations.Count(),

                //We grab 5 vehicles (due to larger cards) and 10 of each customer and reservation to show on dashboard
                Vehicles = vehicles.Take(5).ToList(),
                Customers = customers.Take(10).ToList(),
                Reservations = reservations.Take(10).ToList(),

                //Filter logic for finding rentals due in the next 7 days
                UpcomingRentals = reservations
                    .Where(reservation => reservation.duedate > now && reservation.duedate <= now.AddDays(7))
                    .OrderBy(reservation => reservation.duedate)
                    .ToList(),

                //All rentals where the current date is past the dueDate and they do not have a return date
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
