using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelsProject2024.BL;
using TravelsProject2024.DAL;
using TravelsProject2024.EN;

namespace TravelsProject2024.WEB.Controllers
{
    public class VehiclesController : Controller
    {
        VehicleBL vehicleBL = new VehicleBL();

        // accion que muestra el listado de vehiculos
        public async Task<IActionResult> Index( Vehicle vehicle = null)
        {
            if (vehicle == null)
                vehicle = new Vehicle();
            if (vehicle.Top_Aux == 0)
                vehicle.Top_Aux = 10;
            else if (vehicle.Top_Aux == 1)
                vehicle.Top_Aux = 0;

            var Vehicles = await vehicleBL.SearchAsync(vehicle);
            ViewBag.Top = vehicle.Top_Aux;

            return View(Vehicles);
        }

        // accion que muestra el detalle de un registro
        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await vehicleBL.GetByIdAsync(new Vehicle { Id = id});
            return View(vehicle);
        }

        // Accion que muestra el formulario para crear una nueva categoria
        public IActionResult Create()
        {
            ViewBag.Error = "Error";
            return View();
        }

        // Accion que recibe los datos del formulario y los envia a la bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Vehicle vehicle)
        {

            try
            {
                int result = await vehicleBL.CreateAsync(vehicle);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(vehicle);

            //if (ModelState.IsValid)
            //{
            //  _context.Add(vehicle);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            //}

        }

        // Accion que muestra el formulario con datos cargados para modificar
        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await vehicleBL.GetByIdAsync(new Vehicle { Id =id });
            ViewBag.Error = "";
            return View(vehicle);
        }

        //  Accion que recibe los datos modificados y los envia a la red
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vehicle vehicle)
        {
            try
            {
                int result = await vehicleBL.UpdateAsync(vehicle);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(vehicle);
            }
        }

        // Accion que muestra los datos para confirmar la eliminacion
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await vehicleBL.GetByIdAsync(new Vehicle { Id = id });
            ViewBag.Error = "";

            return View(vehicle);
        }

        // accion que recibe la confirmacion para eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Vehicle vehicle)
        {
            try
            {
                int result = await vehicleBL.DeleteAsync(vehicle);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(vehicle);
            }
        }

        
    }
}
