using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Autor.DTO
{
    public class AutorDTO
    {
        public int id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public DateTime? birthDate { get; set; }

        public string guid { get; set; }

        public List<GradoDTO> grade { get; set; }
    }
}
