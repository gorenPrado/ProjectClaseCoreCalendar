using ProjectClaseCore.Data;
using ProjectClaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectClaseCore.Repositorio
{
    public class RepositorioProject
    {
        ContextoProject context;
        public RepositorioProject(ContextoProject context)
        {
            this.context = context;
        }
        public List<Eventos> GetEventos()
        {
            return context.Eventos.ToList();
        }
        public Eventos BuscarEventos(int idevento)
        {
            return context.Eventos.SingleOrDefault(x => x.IdEvento == idevento);
        }
        public List<Usuarios> GetUsuarios()
        {
            return context.Usuarios.ToList();
        }
        public List<Eventos> EventosUsuario(int idusuario)
        {
            return context.Eventos.Where(x => x.IdUsuario == idusuario).ToList();
        }
        public Usuarios BuscarUsuarios(int idusuario)
        {
            return context.Usuarios.SingleOrDefault(x=>x.IdUsuario==idusuario);
        }
        public Usuarios ExisteUsuario(String correo,int idusario)
        {
            return context.Usuarios.SingleOrDefault(x => x.IdUsuario==idusario && x.Correo == correo);
        }
        public List<Eventos> EventosCalendar(int idusuario)
        {
            return context.Eventos.Where(x => x.IdUsuario==idusuario).ToList();
        }
        public void CrearUsuarios(String nombre, String oficio, String nac, String correo)
        {
            Usuarios cliente = new Usuarios();
            cliente.IdUsuario = context.Eventos.Max(x => x.IdUsuario) + 1;
            cliente.Nombre = nombre;
            cliente.Oficio = oficio;
            cliente.Nacionalidad = nac;
            cliente.Correo = correo;
            context.Usuarios.Add(cliente);
            context.SaveChanges();
        }
        public void CrearEventos(int idevento, DateTime fechai, DateTime fechaf, String descri, int idusuario)
        {
            Eventos evento = new Eventos();
            evento.IdEvento = context.Eventos.Max(x=>x.IdEvento)+1;
            evento.FechaInc = fechai;
            evento.FechaFnl = fechaf;
            evento.Descripcion = descri;
            evento.IdUsuario = idusuario;
            context.Eventos.Add(evento);
            context.SaveChanges();
        }
        public void ModificarEvento(int idevento, DateTime fechainc, DateTime fechafnl, String descripcion, int idusuario)
        {
            Eventos evento = BuscarEventos(idevento);
            evento.FechaInc = fechainc;
            evento.FechaFnl = fechafnl;
            evento.Descripcion = descripcion;
            evento.IdUsuario = idusuario;
            context.SaveChanges();
        }
        public void Eliminar(int idevento)
        {
            Eventos evento = BuscarEventos(idevento);
            context.Eventos.Remove(evento);
            context.SaveChanges();
        }
    }
}
