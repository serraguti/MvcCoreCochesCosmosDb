using Microsoft.AspNetCore.Mvc;
using MvcCoreCochesCosmosDb.Models;
using MvcCoreCochesCosmosDb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCochesCosmosDb.Controllers
{
    public class VehiculosController : Controller
    {
        private ServiceVehiculosCosmos service;

        public VehiculosController(ServiceVehiculosCosmos service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Vehiculo> vehiculos = 
                await this.service.GetVehiculosAsync();
            return View(vehiculos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehiculo car)
        {
            await this.service.AddVehiculoAsync(car);
            return RedirectToAction("Index");
        }
    }
}
