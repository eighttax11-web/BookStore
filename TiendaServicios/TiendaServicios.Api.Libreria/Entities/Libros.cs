using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Entities;

namespace TiendaServicios.Api.Libreria.Entities
{
    public class Libros
    {
        public int LibrosId { get; set; }

        public string TituloLibro { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public string AutorLibro { get; set; }

        public string LibroGuid { get; set; }
    }
}
