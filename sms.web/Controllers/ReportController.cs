using Microsoft.AspNetCore.Mvc;

namespace sms.web.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
