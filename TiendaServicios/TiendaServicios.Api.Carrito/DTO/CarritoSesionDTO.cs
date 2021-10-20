using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Carrito.DTO
{
    public class CarritoSesionDTO
    {
        public int id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string Guid { get; set; }

        public List<CarritoSesionDetalleDTO> ListDetailCart { get; set; }
    }
}
