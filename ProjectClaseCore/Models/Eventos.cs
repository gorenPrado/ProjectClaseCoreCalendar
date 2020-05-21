using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectClaseCore.Models
{
    [Table("Eventos")]
    public class Eventos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Id_Evento")]
        public int IdEvento { get; set; }
        [Column("Fecha_Inicio")]
        public DateTime FechaInc { get; set; }
        [Column("Fecha_Final")]
        public DateTime FechaFnl { get; set; }
        [Column("Descripcion_Evento")]
        public String Descripcion { get; set; }
        [Column("Id_Usuario")]
        public int IdUsuario  { get; set; }
    }
}
