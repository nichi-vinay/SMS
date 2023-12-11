using Microsoft.AspNetCore.Mvc;

namespace sms.web.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Views()
        {
            // Your logic to retrieve and display the data for the view
            return View();
        }
    }
}
