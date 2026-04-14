using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Create()
        {
            ViewBag.Customers = _customerRepository.GetAllCustomers();
            ViewBag.Vehicles = _vehicleRepository.GetAllVehicles();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            /*
                NEW RESERVATION OBJECT IS CREATED AFTER AVAILABILITY VALIDATION
            */
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reservation = _reservationRepository.GetReservationById(id);

            if(!reservation) return NotFound();
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReservationDTO reservation)
        {
            var reservation = new Reservation
            {
                /*
                    RESERVATION OBJECT IS CREATED FROM RESERVATIONDTO FIELDS GRABBED FROM FORM SUBMISSION
                */
            };

            await _reservationRepository.UpdateReservation(id, reservation);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _reservationRepository.GetReservationById(id);
            if(!reservation) return NotFound();

            await _reservationRepository.DeleteReservation(id);

            return RedirectToAction("Index");
        }
    }
}
