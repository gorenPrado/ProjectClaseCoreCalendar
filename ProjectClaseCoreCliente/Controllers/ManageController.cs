using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectClaseCore.Models;
using ProjectClaseCoreCliente.Repositorio;

namespace ProjectClaseCoreCliente.Controllers
{
    public class ManageController : Controller
    {
        RepositorioCalendar repo;
        public ManageController(RepositorioCalendar repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(String username, String password)
        {
            String token = await repo.GetToken(username, password);
            if (token == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
            }
            else
            {
                Usuarios usuario = await repo.Perfil(token);
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Correo));
                //identity.AddClaim(new Claim(ClaimTypes.Role, cliente.UserName));
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddMinutes(60)
                });
                HttpContext.Session.SetString("TOKEN", token);
                return RedirectToAction("Index", "Calendario");
            }
            return View();
        }
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (HttpContext.Session.GetString("TOKEN") != null)
            {
                HttpContext.Session.SetString("TOKEN", String.Empty);
            }
            return RedirectToAction("Login", "Manage");
        }
    }
}