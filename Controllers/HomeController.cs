using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tutorias_Unphu.Models;

namespace Tutorias_Unphu.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}