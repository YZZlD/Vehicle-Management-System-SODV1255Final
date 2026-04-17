using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.src.Models;
using VehicleManagementSystem.src.Repositories;

namespace VehicleManagementSystem.Controllers
{
    public class VehicleController : BaseController
    {
        private readonly VEHICLEREPO _vehicleRepository;

        public VehicleController(VEHICLEREPO vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleRepository.Index();

            return View(vehicles);
        }

        public async Task<IActionResult> Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(VehicleDTO vehicleDTO)
        {
            var vehicle = new VEHICLEMODEL
            {
                make = vehicleDTO.Make,
                model = vehicleDTO.Model,
                licenseplate = vehicleDTO.LicensePlate,
                price = vehicleDTO.PriceRate,
                imagelinkplaintext = vehicleDTO.ImageURL
            };

            await _vehicleRepository.AddVehicle(vehicle);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleRepository.Getbyid(id);
            if(vehicle == null) return NotFound();
            return View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VehicleDTO vehicleDTO)
        {
            var vehicle = await _vehicleRepository.Getbyid(id);

            vehicle.make = vehicleDTO.Make;
            vehicle.model = vehicleDTO.Model;
            vehicle.licenseplate = vehicleDTO.LicensePlate;
            vehicle.price = vehicleDTO.PriceRate;
            vehicle.imagelinkplaintext = vehicleDTO.ImageURL;

            await _vehicleRepository.EditVehicle(vehicle);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _vehicleRepository.Getbyid(id);
            if(vehicle == null) return NotFound();

            return View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            await _vehicleRepository.DeleteVehicle(id);

            return RedirectToAction("Index");
        }
    }
}
