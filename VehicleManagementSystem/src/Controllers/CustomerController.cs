using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.src.Models;
using VehicleManagementSystem.src.Repositories;

namespace VehicleManagementSystem.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly USERREPO _customerRepository;

        public CustomerController(USERREPO customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepository.GetAllUsers();

            return View(customers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDTO customerDTO)
        {
            var customer = new USERMODEL
            {
                fname = customerDTO.FirstName,
                lname = customerDTO.LastName,
                phonenumber = customerDTO.PhoneNumber,
                email = customerDTO.Email,
                age = customerDTO.Age
            };

            await _customerRepository.AddCustomer(customer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerRepository.getuserbyid(id);
            //THIS WILL BE REPLACED FOR A 404 ROUTE FOR HANDLING NOT FOUND
            //MOST LIKELY GOING TO BE DONE THROUGH REDIRECTSWITHSTATUSCODES SO THIS SHOULD NOT CHANGE
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerDTO customerDTO)
        {
            var customer = await _customerRepository.getuserbyid(id);

            customer.fname = customerDTO.FirstName;
            customer.lname = customerDTO.LastName;
            customer.phonenumber = customerDTO.PhoneNumber;
            customer.email = customerDTO.Email;

            await _customerRepository.Edit(customer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepository.getuserbyid(id);
            if(customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerRepository.Deletebyid(id);

            return RedirectToAction("Index");
        }
    }
}
