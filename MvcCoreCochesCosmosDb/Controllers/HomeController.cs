using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcCoreCochesCosmosDb.Models;
using MvcCoreCochesCosmosDb.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCochesCosmosDb.Controllers
{
    public class HomeController : Controller
    {
        private ServiceVehiculosCosmos service;

        public HomeController
            (ServiceVehiculosCosmos service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string algo)
        {
            await this.service.CreateDatabaseAsync();
            ViewData["MENSAJE"] = "Esto funciona!!! Eureka";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
