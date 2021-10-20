using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libreria.DTO
{
    public class LibroDTO
    {
        public int id { get; set; }

        public string titleBook { get; set; }

        public DateTime? releaseDate { get; set; }

        public string autorBook { get; set; }

        public string guid { get; set; }
    }
}
