using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Helpers;
using VehicleManagementSystem.src.Models;
using VehicleManagementSystem.src.Repositories;

namespace VehicleManagementSystem.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly RESERVEREPO _reservationRepository;
        private readonly USERREPO _customerRepository;
        private readonly VEHICLEREPO _vehicleRepository;

        public ReservationController(RESERVEREPO reservationRepository, USERREPO customerRepository, VEHICLEREPO vehicleRepository)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationRepository.index();

            return View(reservations);
        }

        public async Task<IActionResult> SelectDate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SelectDate(DateTime reservedDate, DateTime dueDate)
        {
            var reservations = await _reservationRepository.index();
            var vehicles = await _vehicleRepository.Index();


            var reservedVehicleIds = reservations
                                        .Where(reservation => reservation.reservedate < dueDate && reservation.duedate > reservedDate)
                                        .Select(r => r.vehicleid)
                                        .Distinct()
                                        .ToList();

            var availableVehicles = vehicles
                                        .Where(vehicle => !reservedVehicleIds.Contains(vehicle.vehicleid))
                                        .ToList();

            ViewBag.Customers = await _customerRepository.GetAllUsers();
            ViewBag.Vehicles = availableVehicles;
            ViewBag.ReservedDate = reservedDate;
            ViewBag.DueDate = dueDate;

            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(int customerId, int vehicleId, DateTime reservedDate, DateTime dueDate)
        {
            var customer = await _customerRepository.getuserbyid(customerId);
            var vehicle = await _vehicleRepository.Getbyid(vehicleId);
            //THIS WILL NOT WORK UNTIL GET BY ID IS IMPLEMENTED

            var reservation = new RESERVATIONMODEL
            {
                userid = customerId,
                vehicleid = vehicleId,
                reservedate = reservedDate,
                duedate = dueDate,
                price = CalculatePrice(vehicle, reservedDate, dueDate, null)
            };

            _reservationRepository.Addreserve(reservation);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reservation = _reservationRepository.getbyid(id);

            if(reservation == null) return NotFound();
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DateTime? returnedDate)
        {
            var reservation = await _reservationRepository.getbyid(id);

            if(reservation == null) return NotFound();

            var vehicle = await _vehicleRepository.Getbyid(reservation.vehicleid);

            reservation.returneddate = returnedDate;

            reservation.price = CalculatePrice(vehicle, reservation.reservedate, reservation.duedate, reservation.returneddate);

            _reservationRepository.Edit(reservation);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            _reservationRepository.Deletebyid(id);

            return RedirectToAction("Index");
        }
    }
}
