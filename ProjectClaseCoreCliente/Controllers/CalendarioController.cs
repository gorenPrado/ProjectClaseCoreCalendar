using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectClaseCore.Models;
using ProjectClaseCoreCliente.Filters;
using ProjectClaseCoreCliente.Repositorio;

namespace ProjectClaseCoreCliente.Controllers
{
    [UsuarioAuthorized]
    public class CalendarioController : Controller
    {
        RepositorioCalendar repo;
        public CalendarioController(RepositorioCalendar repo)
        {
            this.repo = repo;
        }
        //Perfil del Usuario        
        public async Task<IActionResult> Index()
        {
            String token = HttpContext.Session.GetString("TOKEN");
            Usuarios usuario = await repo.Perfil(token);
            return View(usuario);
        }
        //Calendario
        public IActionResult Calendario()
        {
            return View();
        }

        //Eventos en forma de listado del Usuario
        public async Task<IActionResult> Eventos()
        {
            String token = HttpContext.Session.GetString("TOKEN");
            List<Eventos> evento = await repo.EventoUsuario(token);
            return View(evento);
        }
        //Insertar evento
        public IActionResult Insertar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insertar(int idevento, DateTime fechainc, DateTime fechafnl, String descripcion)
        {
            String token = HttpContext.Session.GetString("TOKEN");
            Usuarios usuario = await repo.Perfil(token);
            Eventos evento = new Eventos();
            evento.IdEvento = idevento;
            evento.FechaInc = fechainc;
            evento.FechaFnl = fechafnl;
            evento.Descripcion = descripcion;
            evento.IdUsuario = usuario.IdUsuario;
            await repo.Insertar(token, evento);
            return RedirectToAction("Eventos");
        }
        //Borrar evento
        public async Task<IActionResult> Eliminar(int idevento)
        {
            String token = HttpContext.Session.GetString("TOKEN");
            await repo.Delete(token,idevento);
            return RedirectToAction("Eventos");
        }
        //Modificar evento
        public async Task<IActionResult> Modificar(int idevento)
        {
            String token = HttpContext.Session.GetString("TOKEN");
            Eventos evento = await repo.BuscarEvento(idevento,token);
            return View(evento);
        }
        [HttpPost]
        public async Task<IActionResult> Modificar(int idevento, DateTime fechainc, DateTime fechafnl, String descripcion)
        {
            String token = HttpContext.Session.GetString("TOKEN");
            Usuarios usuario = await repo.Perfil(token);
            Eventos evento = new Eventos();
            evento.FechaInc = fechainc;
            evento.FechaFnl = fechafnl;
            evento.Descripcion = descripcion;
            evento.IdUsuario = usuario.IdUsuario;
            await repo.Modificar(token, evento);
            return RedirectToAction("Eventos");
        }
        //Crear Usuario
        public IActionResult CrearUsuario()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearUsuario(int idusuario, String nombre, String oficio, String nac, String correo)
        {
            String token = HttpContext.Session.GetString("TOKEN");
            Usuarios usu = await repo.Perfil(token);
            Usuarios usuario = new Usuarios();
            usuario.IdUsuario = idusuario;
            usuario.Nombre = nombre;
            usuario.Oficio = oficio;
            usuario.Nacionalidad = nac;
            usuario.Correo = correo;
            await repo.InsertarUsuario(token, usuario);
            return RedirectToAction("Eventos");
        }
        public IActionResult Prueba()
        {
            String token = HttpContext.Session.GetString("TOKEN");
            ViewData["TOKEN"] = token;
            return View();
        }
    }
}