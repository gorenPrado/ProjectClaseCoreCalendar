using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectClaseCore.Models
{    
    public class Eventos
    {
        [JsonProperty("idEvento")]
        public int IdEvento { get; set; }
        [JsonProperty("fechaInc")]
        public DateTime FechaInc { get; set; }
        [JsonProperty("fechaFnl")]
        public DateTime FechaFnl { get; set; }
        [JsonProperty("descripcion")]
        public String Descripcion { get; set; }
        [JsonProperty("idUsuario")]
        public int IdUsuario  { get; set; }
    }
}
