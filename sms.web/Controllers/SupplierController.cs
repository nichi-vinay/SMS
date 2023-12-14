using Microsoft.AspNetCore.Mvc;

namespace sms.web.Controllers
{
    public class SupplierController : Controller
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
    }
}
