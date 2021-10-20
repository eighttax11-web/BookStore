using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Carrito.DTO
{
    public class CarritoSesionDetalleDTO
    {
        public int id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string SelectedProduct { get; set; }

        public string Guid { get; set; }
    }
}
