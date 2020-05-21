using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectClaseCore.Models
{
    [Table("Usuarios")]
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Id_Usuario")]
        public int IdUsuario { get; set; }
        [Column("Nombre_Usuario")]
        public String Nombre { get; set; }
        [Column("Oficio")]
        public String Oficio { get; set; }
        [Column("Nacionalidad")]
        public String Nacionalidad { get; set; }
        [Column("Correo")]
        public String Correo { get; set; }
    }
}
