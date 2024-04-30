using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrosMaui.Models.DTOs
{
    public class LibroDTO
    {
        public string Titulo { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public string Portada { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int? Id { get; set; }
        public bool Eliminado { get; set; }
    }
}
