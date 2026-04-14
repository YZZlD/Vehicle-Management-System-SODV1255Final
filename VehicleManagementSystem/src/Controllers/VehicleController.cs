using Microsoft.AspNetCore.Mvc;

namespace VehicleManagementSystem.Controllers
{
    public class VehicleController : Controller
    {
        private readonly VehicleRepository _vehicleRepository;

        public VehicleController(VehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleRepository.GetAllVehicles();

            return View(vehicles);
        }

        public async Task<IActionResult> Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(VehicleController DTO)
        {
            var vehicle = new Vehicle
            {
                /*
                    VEHICLE OBJECT IS CREATED FROM VEHICLEDTO FIELDS FROM FORM SUBMISSION
                */
            };

            await _vehicleRepository.AddVehicle(vehicle);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(id);
            if(!vehicle) return NotFound();
            return View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VehicleDTO vehicle)
        {
            var vehicle = new Vehicle
            {
                /*
                    VEHICLE OBJECT IS CREATED FROM VEHICLEDTO FIELDS GRABBED FROM FORM SUBMISSION
                */
            };

            await _vehicleRepository.UpdateVehicle(id, vehicle);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(id);
            if(!vehicle) return NotFound();

            await _vehicleRepository.DeleteVehicle(id);

            return RedirectToAction("Index");
        }
}
