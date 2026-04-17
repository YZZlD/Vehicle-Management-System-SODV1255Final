using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleManagementSystem.Helpers;
using VehicleManagementSystem.src.Models;
using VehicleManagementSystem.src.Repositories;
using System.Linq;

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

            var users = await _customerRepository.GetAllUsers();
            ViewBag.Customers = users.Select(user => new SelectListItem
            {
                Value = user.userid.ToString(),
                Text = user.fname + user.lname
            }).ToList();

            ViewBag.Vehicles = availableVehicles.Select(vehicle => new SelectListItem
            {
                Value = vehicle.vehicleid.ToString(),
                Text = vehicle.make + vehicle.model
            }).ToList();

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
                price = PricingHelper.CalculatePrice(vehicle, reservedDate, dueDate, null)
            };

            await _reservationRepository.Addreserve(reservation);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _reservationRepository.getbyid(id);

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

            reservation.price = PricingHelper.CalculatePrice(vehicle, reservation.reservedate, reservation.duedate, reservation.returneddate);

            await _reservationRepository.Edit(reservation);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _reservationRepository.getbyid(id);
            if(reservation == null) return NotFound();

            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservationRepository.Deletebyid(id);

            return RedirectToAction("Index");
        }
    }
}
