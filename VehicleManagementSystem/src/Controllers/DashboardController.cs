using Microsoft.AspNetCore.Mvc;

namespace VehicleManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        // private readonly CustomerRepository _customerRepository;

        // private readonly VehicleRepository _vehicleRepository;

        // private readonly ReservationRepository _reservationRepository;

        // public DashboardController(CustomerRepository customerRepository, VehicleRepository vehicleRepository, ReservationRepository reservationRepository)
        // {
        //     _customerRepository = customerRepository;
        //     _vehicleRepository = vehicleRepository;
        //     _reservationRepository = reservationRepository;
        // }

        public async Task<IActionResult> Index()
        {
            /*
                REPOSITORY SHOULD BE CALLED IN INDEX TO GET THE FOLLOWING ITEMS FOR THE DASHBOARD:

                    - Most recent items for each model (count of 3)
                    - All vehicle availability information
                    - Upcoming reservation expirations/return dates
                    - Overdue reservation return dates

                ALL NECESSARY INFORMATION WILL BE PASSED AND RENDERED IN THE DASHBOARD VIEW INDEX
            */

            return View();
        }
    }
}
