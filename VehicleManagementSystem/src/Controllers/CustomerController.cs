using Microsoft.AspNetCore.Mvc;
// using VehicleManagementSystem.Repositories;
// using VehicleManagementSystem.Models;

namespace VehicleManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        //private readonly CustomerRepository _customerRepository;

        // public CustomerController(CustomerRepository customerRepository)
        // {
        //     _customerRepository = customerRepository;
        // }

        public async Task<IActionResult> Index()
        {
            // var customers = await _customerRepository.GetAllCustomers();

            // return View(customers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDTO customer)
        {
            var customer = new Customer
            {
                /*
                    CUSTOMER OBJECT IS CREATED FROM CUSTOMERDTO FIELDS GRABBED FROM FORM SUBMISSION
                */
            };

            await _customerRepository.AddCustomer(customer);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var customer = _customerRepository.GetCustomerByID(id);
            //THIS WILL BE REPLACED FOR A 404 ROUTE FOR HANDLING NOT FOUND
            //MOST LIKELY GOING TO BE DONE THROUGH REDIRECTSWITHSTATUSCODES SO THIS SHOULD NOT CHANGE
            if (!customer) return NotFound();
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerDTO customer)
        {
            var customer = new Customer
            {
                /*
                    CUSTOMER OBJECT IS CREATED FROM CUSTOMERDTO FIELDS GRABBED FROM FORM SUBMISSION
                */
            };

            await _customerRepository.UpdateCustomer(id, customer);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = _customerRepository.GetCustomerByID(id);
            if(!customer) return NotFound();

            await _customerRepository.DeleteCustomer(id);

            return RedirectToAction("Index");
        }
    }
}
