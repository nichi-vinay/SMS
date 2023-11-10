using Microsoft.AspNetCore.Mvc;

namespace sms.web.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LoadTable(string option)
        {
            // Depending on the 'option' parameter, return the corresponding partial view
            switch (option)
            {
                case "Inventory":
                    return PartialView("Inventory"); // Replace with the actual partial view name
                case "Sales":
                    return PartialView("Sales"); // Replace with the actual partial view name
                case "Purchases":
                    return PartialView("Purchases"); // Replace with the actual partial view name
                case "Customer":
                    return PartialView("Customer"); // Replace with the actual partial view name
                case "Enquiry":
                    return PartialView("Enquiry"); // Replace with the actual partial view name
                case "Supplier":
                    return PartialView("Supplier"); // Replace with the actual partial view name
                default:
                    return Content("Invalid option");
            }
        }
    }
}
