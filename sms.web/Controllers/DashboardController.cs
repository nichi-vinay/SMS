using Microsoft.AspNetCore.Mvc;

namespace sms.web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
