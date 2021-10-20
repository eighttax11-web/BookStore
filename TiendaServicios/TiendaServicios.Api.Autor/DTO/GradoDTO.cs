using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Entities;

namespace TiendaServicios.Api.Autor.DTO
{
    public class GradoDTO
    {
        public int id { get; set; }

        public string name { get; set; }

        public string academicCenter { get; set; }

        public DateTime? dateGrade { get; set; }

        public string guid { get; set; }
    }
}
