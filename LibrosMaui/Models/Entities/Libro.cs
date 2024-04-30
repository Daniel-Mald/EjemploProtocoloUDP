using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
//using SQLite.Net.Attributes;
//using TableAttribute = SQLite.Net.Attributes.TableAttribute;
using SQLite;

namespace LibrosMaui.Models.Entities
{
    [SQLite.Table("Libros")]
    public class Libro
    {
        [PrimaryKey]
        public int Id { get; set; }
        [NotNull]
        public string Titulo { get; set; } = null!;
        [NotNull]
        public string Autor { get; set; } = null!;
        public bool Eliminado { get; set; }
        [NotNull]
        public string Portada { get; set; } = null!;
    }
}
