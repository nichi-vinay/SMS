using Microsoft.AspNetCore.Mvc;

namespace sms.web.Controllers
{
    public class PurchaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddItem()
        {
            return View();
        }
        public IActionResult AddPurchase()
        {
            return View();
        }
    }
}
