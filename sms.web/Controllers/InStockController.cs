using Microsoft.AspNetCore.Mvc;

namespace sms.web.Controllers
{
    public class InStockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
