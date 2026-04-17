using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;

namespace VehicleManagementSystem.Controllers
{
    public class ReservationController : Controller
    {
        // private readonly ReservationRepository _reservationRepository;
        // private readonly CustomerRepository _customerRepository;
        // private readonly VehicleRepository _vehicleRepository;

        // public ReservationController(ReservationRepository reservationRepository, CustomerRepository customerRepository, VehicleRepository vehicleRepository)
        // {
        //     _reservationRepository = reservationRepository;
        //     _customerRepository = customerRepository;
        //     _vehicleRepository = vehicleRepository;
        // }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationRepository.GetAllReservations();

            return View(reservations);
        }

        public async Task<IActionResult> SelectDate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SelectDate(DateTime reservedDate, DateTime dueDate)
        {
            var reservedVehicleIds = await _reservationRepository.GetAllReservations()
                                        .Where(r => r.ReservedDate < dueDate && r.DueDate > reservedDate)
                                        .Select(r => r.VehicleId)
                                        .Distinct()
                                        .ToList();

            var availableVehicles = await _vehicleRepository.GetAllVehicles()
                                        .Where(vehicle => !reservedVehicleIds.Contains(vehicle.VehicleId))
                                        .ToList();

            ViewBag.Customers = await _customerRepository.GetAllCustomers();
            ViewBag.Vehicles = availableVehicles;
            ViewBag.ReservedDate = reservedDate;
            ViewBag.DueDate = dueDate;

            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(int customerId, int vehicleId, DateTime reservedDate, DateTime dueDate)
        {
            var customer = await _customerRepository.GetCustomerById(customerId);
            var vehicle = await _vehicleRepository.GetVehicleById(vehicleId);

            var reservation = new Reservation
            {
                CustomerId = customerId,
                VehicleId = vehicleId,
                ReservedDate = reservedDate,
                DueDate = dueDate,
                PriceTotal = CalculatePrice(vehicle, reservedDate, dueDate)
            };

            await _reservationRepository.Add(reservation);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _reservationRepository.GetReservationById(id);

            if(reservation == null) return NotFound();
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DateTime? returnedDate)
        {
            var reservation = await _reservationRepository.GetReservationById(id);

            if(reservation == null) return NotFound();

            var vehicle = await _vehicleRepository.GetVehicleById(reservation.VehicleId);

            reservation.ReturnedDate = returnedDate;

            reservation.PriceTotal = CalculatePrice(vehicle, reservation.ReservedDate, reservation.DueDate, reservation.ReturnedDate);

            await _reservationRepository.UpdateReservation(reservation);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _reservationRepository.DeleteReservation(id);

            return RedirectToAction("Index");
        }
    }
}
