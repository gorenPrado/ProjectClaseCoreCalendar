using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectClaseCore.Models;
using ProjectClaseCore.Repositorio;

namespace ProjectClaseCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        RepositorioProject repo;
        public ProjectController(RepositorioProject repo)
        {
            this.repo = repo;
        }
        //[Authorize]
        [HttpGet("[action]")]
        public ActionResult<List<Eventos>> GetEventos()
        {
            return repo.GetEventos();
        }
        //[Authorize]
        [HttpGet("[action]/{idevento}")]
        public ActionResult<Eventos> BuscarEvento(int idevento)
        {
            return repo.BuscarEventos(idevento);
        }
        //[Authorize]
        [HttpGet("[action]")]
        public ActionResult<List<Usuarios>> GetUsuario()
        {
            return repo.GetUsuarios();
        }
        [Authorize]
        [HttpGet("[action]")]
        public ActionResult<Usuarios> Perfil()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            String json = claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Usuarios usu = JsonConvert.DeserializeObject<Usuarios>(json);
            return usu;
        }
        [Authorize]
        [HttpGet("[action]")]
        public ActionResult<List<Eventos>> EventoUsuario()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            String json = claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Usuarios usu = JsonConvert.DeserializeObject<Usuarios>(json);
            List<Eventos> pedidos = repo.EventosUsuario(usu.IdUsuario);
            return pedidos;
        }
        [Authorize]
        [HttpPost("[action]")]
        public void CrearUsuario(Usuarios usuario)
        {
            repo.CrearUsuarios(usuario.Nombre, usuario.Oficio, usuario.Nacionalidad, usuario.Correo);
        }
        [Authorize]
        [HttpPost("[action]")]
        public void CrearEvento(Eventos evento)
        {
            repo.CrearEventos(evento.IdEvento, evento.FechaInc, evento.FechaFnl, evento.Descripcion, evento.IdUsuario);
        }
        [Authorize]
        [HttpDelete("[action]/{idevento}")]
        public void EliminarEvento(int idevento)
        {
            repo.Eliminar(idevento);
        }
        [Authorize]
        [HttpPut("[action]")]
        public void ModificarEvento(Eventos evento)
        {
            repo.ModificarEvento(evento.IdEvento, evento.FechaInc, evento.FechaFnl, evento.Descripcion, evento.IdUsuario);
        }
        [Authorize]
        [HttpGet ("[action]")]
        public ActionResult<List<Eventos>> EventosCalendar()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            String json = claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Usuarios usu = JsonConvert.DeserializeObject<Usuarios>(json);
            List<Eventos> eventos = repo.EventosCalendar(usu.IdUsuario);
            return eventos;
        }

    }
}