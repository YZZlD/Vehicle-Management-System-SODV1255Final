using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using VehicleManagementSystem.Repositories;
// using VehicleManagementSystem.Models;

namespace VehicleManagementSystem.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepository.GetAllCustomers();

            return View(customers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Phone = customerDTO.Phone,
                Email = customerDTO.Email
            };

            await _customerRepository.AddCustomer(customer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerRepository.GetCustomerByID(id);
            //THIS WILL BE REPLACED FOR A 404 ROUTE FOR HANDLING NOT FOUND
            //MOST LIKELY GOING TO BE DONE THROUGH REDIRECTSWITHSTATUSCODES SO THIS SHOULD NOT CHANGE
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Phone = customerDTO.Phone,
                Email = customerDTO.Email
            };

            await _customerRepository.UpdateCustomer(id, customer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepository.GetCustomerByID(id);
            if(customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerRepository.DeleteCustomer(id);

            return RedirectToAction("Index");
        }
    }
}
