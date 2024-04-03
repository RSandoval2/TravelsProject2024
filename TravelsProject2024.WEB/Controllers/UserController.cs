
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data;
using System.Security.Claims;
using TravelsProject2024.BL;
using TravelsProject2024.BussinessLogic;
using TravelsProject2024.EN;

namespace TravelsProject2024.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]

    public class UserController : Controller
    {
        UserBL UserBL = new UserBL();
        RoleBL RoleBL = new RoleBL();

        // Accion que muestra la lista de usuarios registrados
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index(User user = null)
        {
            if (user == null)
                user = new User();
            if (user.Top_Aux == 0)
                user.Top_Aux = 10; //numero de registros a mostrar´por defecto
            else if (user.Top_Aux == -1)
                user.Top_Aux = 0;

            var users = await UserBL.SearchIncludeRoleAsync(user);
            var roles = await RoleBL.GetAllAsync();

            ViewBag.Roles = roles;
            ViewBag.Top = user.Top_Aux;

            return View(users);
        }

        //Accion que muestra el detalle de un registro
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Details(int id)
        {
            var user = await UserBL.GetByIdAsync(new User { Id = id });
            user.Role = await RoleBL.GetByIdAsync(new Role { Id = user.IdRole });
            return View(user);
        }

        // Accion que muestra el formulario para un registro nuevo
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Create()
        {
            var roles = await RoleBL.GetAllAsync();
            ViewBag.Roles = roles;
            return View();
        }

        // Accion que recibe los datos y los envia a la bd mediante el modelo
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Create(User user)
        {
            try
            {
                int result = await UserBL.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = await RoleBL.GetAllAsync();
                return View(user);
            }
        }

        // Accion que muestra el formulario de los datos cargados para editar
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Edit(int id)
        {
            var user = await UserBL.GetByIdAsync(new User { Id = id });
            user.Role = await RoleBL.GetByIdAsync(new Role { Id = user.Id });
            ViewBag.Roles = await RoleBL.GetAllAsync();
            return View(user);
        }

        // accion que recibe los datos modificados y los envia a la bd
        [Authorize(Roles = "Administrador")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            try
            {
                int result = await UserBL.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = await RoleBL.GetAllAsync();
                return View(user);
            }
        }

        // acción que muestra los datos para confirmar la eliminación
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Delete(int id)
        {
            var user = await UserBL.GetByIdAsync(new User { Id = id });
            user.Role = await RoleBL.GetByIdAsync(new Role { Id = user.Id });
            return View(user);
        }

        // accion que recibe la confirmacion para eliminar 
        [Authorize(Roles = "Administrador")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, User user)
        {
            try
            {
                int result = await UserBL.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var userDb = await UserBL.GetByIdAsync(user);
                if (userDb == null)
                    userDb = new User();
                if (userDb.Id > 0)
                    userDb.Role = await RoleBL.GetByIdAsync(new Role { Id = userDb.Id });
                return View(userDb);
            }
        }

        // accion que muestra el formulario de iinicio de sesión 
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = returnUrl;
            ViewBag.Error = "";
            return View();
        }

        //accion que ejecuta la autenticacion del usuario
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user, string returnUrl = null)
        {
            try
            {
                var userDb = await UserBL.LoginAsync(user);
                if (userDb != null && userDb.Id > 0 && userDb.Login == user.Login)
                {
                    userDb.Role = await RoleBL.GetByIdAsync(new Role { Id = userDb.IdRole });

                    var claims = new[] { new Claim(ClaimTypes.Name, userDb.Login), new Claim(ClaimTypes.Role, userDb.Role.Name) };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                }
                else
                    throw new Exception("Hay un problema con sus credenciales");

                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "TouristPlace");
            }
            catch (Exception ex)
            {
                ViewBag.Url = returnUrl;
                ViewBag.Error = ex.Message;
                return View(new User { Login = user.Login });
            }
        }

        //accion que permite cerrar la sesion del usuario
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

        //accion que muestra el formulario para cambiar contraseña
        public async Task<IActionResult> ChangePassword()
        {
            var users = await UserBL.SearchAsync(new User { Login = User.Identity.Name, Top_Aux = 1 });
            var actualUser = users.FirstOrDefault();
            return View(actualUser);
        }

        //Ación que recibe la contraseña actualizada y la envia a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(User user, string oldPassword)
        {
            try
            {
                int result = await UserBL.ChangePasswordAsync(user, oldPassword);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var users = await UserBL.SearchAsync(new User { Login = User.Identity.Name, Top_Aux = 1 });
                var actualUser = users.FirstOrDefault();
                return View(actualUser);
            }
        }

        //Acción que permite mostrar el formulario para el registro de usuario
        [AllowAnonymous]
        public IActionResult Register() 
        {
            return View();
        }

        //Acción que recibe los datos del registro de los usuarios
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User usuario)
        {
            try
            {
                usuario.IdRole = 2;
                usuario.Status = (byte)User_Status.ACTIVO;
                int result = await UserBL.CreateAsync(usuario);
                return RedirectToAction("Login", "Usuario");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(usuario);
            }
        }
    }
}
