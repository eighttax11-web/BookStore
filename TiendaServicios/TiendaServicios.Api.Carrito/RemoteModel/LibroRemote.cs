using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Carrito.RemoteModel
{
    public class LibroRemote
    {
        public int LibrosId { get; set; }

        public string TituloLibro { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public string AutorLibro { get; set; }

        public string LibroGuid { get; set; }
    }
}
