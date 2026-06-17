using CarCRM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarCRM.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LandingPage()
        {
            return View();
        }

    }
}
