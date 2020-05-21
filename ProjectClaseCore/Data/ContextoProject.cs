using Microsoft.EntityFrameworkCore;
using ProjectClaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectClaseCore.Data
{
    public class ContextoProject:DbContext
    {
        public ContextoProject(DbContextOptions options) : base(options) { }
        public DbSet<Eventos> Eventos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
