using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TravelsProject2024.BL;
using TravelsProject2024.EN;
using TravelsProject2024.WEB.Helpers;
using TravelsProject2024.DAL;

namespace AdsProject.GraphicUserInterface.Controllers
{
    public class TouristPlaceController : Controller
    {
        TouristPlaceBL touristPlaceBL = new TouristPlaceBL();
        TouristPlaceImageBL touristPlaceImageBL = new TouristPlaceImageBL();

        //Acción que muestra la página principal de lugares turísticos
        public async Task<IActionResult> Index(TouristPlaces touristPlace = null)
        {
           if (touristPlace == null)
                touristPlace = new TouristPlaces();
            if (touristPlace.Top_Aux == 0)
                touristPlace.Top_Aux = 10;
            else if (touristPlace.Top_Aux == -1)
                touristPlace.Top_Aux = 0;

            var touristPlaces = await touristPlaceBL.SearchAsync(touristPlace);

            ViewBag.Top = touristPlace.Top_Aux;
            return View(touristPlaces);
        }

        // Acción que muestra los detalles de un lugar turístico
        public async Task<IActionResult> Details(int id)
        {
            var touristPlace = await touristPlaceBL.GetByIdAsync(id);
            touristPlace.TouristPlacesImages = await touristPlaceImageBL.SearchAsync(new TouristPlaceImage() { IdTouristPlaces = touristPlace.Id });
            return View(touristPlace);
        }

        // Acción que muestra el formulario para crear un nuevo lugar turístico
        public async Task<IActionResult> Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // Acción que recibe los datos del formulario y los envía a la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TouristPlaces touristPlace, List<IFormFile> formFiles)
        {
            try
            {
                List<TouristPlaceImage> images = new List<TouristPlaceImage>(); // Declaración de la lista para almacenar las imágenes

                foreach (IFormFile file in formFiles) // Recorremos en caso de que vengan dos o más imágenes
                {
                    TouristPlaceImage touristPlaceImage = new TouristPlaceImage();
                    touristPlaceImage.Id = touristPlace.Id;
                    touristPlaceImage.Path = await ImageHelper.SubirArchivo(file.OpenReadStream(), file.FileName);
                    images.Add(touristPlaceImage);
                }

                touristPlace.TouristPlacesImages = images;
                int result = await touristPlaceBL.CreateAsync(touristPlace);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(touristPlace);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var TouristPlace = await touristPlaceBL.GetByIdAsync(id);
            ViewBag.Error = "";
            return View(TouristPlace);
        }

        //  Accion que recibe los datos modificados y los envia a la red
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TouristPlaces touristPlaces)
        {
            try
            {
                int result = await touristPlaceBL.UpdateAsync(touristPlaces);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(touristPlaces);
            }
        }






        // Acción que muestra los datos del registro para confirmar la eliminación
        public async Task<IActionResult> Delete(TouristPlaces touristPlace)
        {
            var touristPlaceDB = await touristPlaceBL.GetByIdAsync(touristPlace.Id);
            ViewBag.Error = "";
            return View(touristPlace);
        }

        // Acción que recibe la confirmación para eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, TouristPlaces touristPlace)
        {
            try
            {
                TouristPlaces touristPlaceDB = await touristPlaceBL.GetByIdAsync(touristPlace.Id);
                touristPlaceDB.TouristPlacesImages = await touristPlaceImageBL.SearchAsync(new TouristPlaceImage() { Id = touristPlaceDB.Id });
                if (touristPlaceDB.TouristPlacesImages.Count() > 0)
                {
                    foreach (var touristPlaceImage in touristPlaceDB.TouristPlacesImages)
                    {
                        await touristPlaceImageBL.DeleteAsync(touristPlaceImage);
                    }
                }
                int result = await touristPlaceBL.DeleteAsync(touristPlaceDB);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var touristPlaceBD = await touristPlaceBL.GetByIdAsync(touristPlace.Id);
                if (touristPlaceBD == null)
                    touristPlaceBD = new TouristPlaces();
                return View(touristPlaceBD);
            }
        }
    }
}
