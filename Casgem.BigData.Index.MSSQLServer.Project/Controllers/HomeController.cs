﻿using Casgem.BigData.Index.MSSQLServer.Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Casgem.BigData.Index.MSSQLServer.Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index_2","CarPlates");
        }

        public IActionResult Privacy()
        {
            return RedirectToAction("Index_2", "CarPlates");
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return RedirectToAction("Index_2", "CarPlates");
		}
    }
}