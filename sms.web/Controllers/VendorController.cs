﻿using Microsoft.AspNetCore.Mvc;

namespace sms.web.Controllers
{
    public class VendorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
