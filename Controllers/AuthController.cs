﻿using Microsoft.AspNetCore.Mvc;

namespace GardenApp.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
