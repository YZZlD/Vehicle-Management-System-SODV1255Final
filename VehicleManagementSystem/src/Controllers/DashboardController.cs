using Microsoft.AspNetCore.Mvc;

namespace VehicleManagementSystem.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly CustomerRepository _customerRepository;
        private readonly VehicleRepository _vehicleRepository;
        private readonly ReservationRepository _reservationRepository;

        public DashboardController(CustomerRepository customerRepository, VehicleRepository vehicleRepository, ReservationRepository reservationRepository)
        {
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleRepository.GetAllVehicles();
            var customers = await _customerRepository.GetAllCustomers();
            var reservations = await _reservationRepository.GetAllReservations();

            var now = DateTime.UtcNow;

            var model = new DashboardViewModel
            {
                Vehicles = vehicles.Take(3).ToList(),
                Customers = customers.Take(3).ToList(),
                Reservations = reservations.Take(3).ToList(),

                UpcomingRentals = reservations
                    .Where(reservation => reservation.DueDate > now && reservation.DueDate <= now.AddDays(7))
                    .OrderBy(reservation => reservation.DueDate)
                    .ToList(),

                OverdueRentals = reservations
                    .Where(reservation => reservation.DueDate < now)
                    .OrderBy(reservation => reservation.DueDate)
                    .ToList()
            };

            return View(model);
        }
    }
}
