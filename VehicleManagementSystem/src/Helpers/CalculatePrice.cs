using VehicleManagementSystem.src.Models

namespace VehicleManagementSystem.Helpers
{

    public static class PricingHelper
    {
        public static double CalculatePrice(VEHICLEMODEL vehicle, DateTime reservedDate, DateTime dueDate, DateTime? returnedDate)
        {
            // Final value represents tax for now will probably add tax to reservation object for different tax juristrictions
            const double taxRate = 1.05;
            double priceTotal = vehicle.pricerate * (dueDate - reservedDate).Days * taxRate;

            if(returnedDate.HasValue && returnedDate.Value > dueDate)
            {
                priceTotal += 50;
            }

            return priceTotal;
        }
    }

}
