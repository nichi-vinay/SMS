using Microsoft.AspNetCore.Mvc;

namespace sms.web.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
