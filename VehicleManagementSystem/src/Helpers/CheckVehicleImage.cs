namespace VehicleManagementSystem.Helpers
{
    public static class ImageValidationHelper
    {
        // Basic validation to check if an image is from a URL, if not use the default image.
        public static string GetVehicleImage(string imageURL)
        {
            if(string.IsNullOrWhiteSpace(imageURL)) return "/images/notFound.jpg";

            if(imageURL.StartsWith("http")) return imageURL;

            return "images/notFound.jpg";
        }
    }
}
