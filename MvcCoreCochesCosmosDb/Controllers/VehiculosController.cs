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

        public IActionResult BuscarVehiculos()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuscarVehiculos(string marca)
        {
            List<Vehiculo> coches =
                await this.service.GetVehiculosMarcaAsync(marca);
            return View(coches);
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
        public async Task<IActionResult> Create(Vehiculo car, string existemotor)
        {
            if (existemotor == null)
            {
                car.Motor = null;
            }
            await this.service.AddVehiculoAsync(car);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            Vehiculo car = await this.service.FindVehiculoAsync(id);
            return View(car);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.service.DeleteVehiculoAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            Vehiculo car = await this.service.FindVehiculoAsync(id);
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vehiculo car)
        {
            await this.service.UpdateVehiculoAsync(car);
            return RedirectToAction("Index");
        }
    }
}
