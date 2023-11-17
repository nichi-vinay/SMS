using Microsoft.AspNetCore.Mvc;

namespace sms.web.Controllers
{
    public class EnquiryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPartialView(string option)
        {
            string partialViewName = GetPartialViewName(option);

            if (partialViewName != null)
            {
                // Your logic to generate the partial view goes here
                return PartialView(partialViewName);
            }
            else
            {
                // Handle an invalid or unknown option
                return Content("Invalid option.");
            }
        }
        // Helper method to map option values to partial view names
        private string GetPartialViewName(string option)
        {
            // Construct the partial view name with an underscore prefix
            return "_" + option + "Table"; // For example, "_CustomerTable" for "Customer"
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
