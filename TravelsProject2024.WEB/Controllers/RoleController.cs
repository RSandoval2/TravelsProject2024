
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelsProject2024.BL;
using TravelsProject2024.EN;

namespace TravelsProject2024.WEB.Controllers
{
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]

    public class RoleController : Controller
    {

        RoleBL roleBL = new RoleBL();

        //accion que muestra el listado de categorias
        public async Task<IActionResult> Index(Role role = null)
        {
            if (role == null)
                role = new Role();
            if (role.Top_Aux == 0)
                role.Top_Aux = 10; //numero de registros a mostrar´por defecto
            else if (role.Top_Aux == -1)
                role.Top_Aux = 0;

            var roles = await roleBL.SearchAsync(role);
            ViewBag.Top = role.Top_Aux;


            return View(roles);
        }

        // Accion que muestra el detalle de un registro
        public async Task<IActionResult> details(int id)
        {
            var role = await roleBL.GetByIdAsync(new Role { Id = id });
            return View(role);
        }

        //accion que muestra el formulario para crear un nuevo rol
        public ActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // Acción que recibe los datos del formulario y los envia a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            try
            {
                int result = await roleBL.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }


        //accion que muestra el formulario con datos cargados para modificar
        public async Task<IActionResult> Edit(int id)
        {
            var role = await roleBL.GetByIdAsync(new Role { Id = id });
            ViewBag.Error = "";
            return View(role);
        }

        // accion que recibe los datos modificados y los envia a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Role role)
        {
            try
            {
                int result = await roleBL.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }

        //Accion que muestra los datos para confirmar la eliminacion
        public async Task<IActionResult> Delete(int id)
        {
            var role = await roleBL.GetByIdAsync(new Role { Id = id });
            ViewBag.Error = "";
            return View(role);
        }

        // Accion que recibe la confirmacion para eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Role role)
        {
            try
            {
                int result = await roleBL.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }
    }
}
