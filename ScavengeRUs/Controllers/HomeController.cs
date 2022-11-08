using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScavengeRUs.Models;
using ScavengeRUs.Models.Entities;
using System.Diagnostics;

namespace ScavengeRUs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// This doesn't really matter to us
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger; 
        }
        /// <summary>
        /// This is the landing page for www.localhost.com/Home/Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();      //Right click and go to view to see the HTML or see it in the Views/Home folder in the solution explorer
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(Hunt hunt)
        {
            return View();
        }
        /// <summary>
        /// This is the landing page for www.localhost.com/Home/Privacy
        /// Only people that are "Admin" can view this 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        public IActionResult Privacy()
        {
            return View();
        }


        /// <summary>
        /// This is the page displayed if there were a error
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}