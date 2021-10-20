using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.Context;
using TiendaServicios.Api.Carrito.DTO;
using TiendaServicios.Api.Carrito.Entities;

namespace TiendaServicios.Api.Carrito.Aplicacion
{
    public class QueryFilter
    {
        public class CarritoUnico : IRequest<CarritoSesionDTO>
        {
            public string CarritoGuid { get; set; }
        }

        public class Manejador : IRequestHandler<CarritoUnico, CarritoSesionDTO>
        {
            private readonly ContextoCarritoSesion context;
            public CarritoSesionDTO carritodto;
            public List<CarritoSesionDetalleDTO> carritodetallesdto;
            public CarritoSesionDetalleDTO carritodetalledto;

            public Manejador(ContextoCarritoSesion context)
            {
                this.context = context;
            }
            public async Task<CarritoSesionDTO> Handle(CarritoUnico request, CancellationToken cancellationToken)
            {
                var carrito = await this.context.CarritoSesion.Include(c => c.ListaDetalle).Where(x => x.CarritoSesionGuid.Equals(request.CarritoGuid)).FirstOrDefaultAsync();

                if (carrito == null)
                    throw new Exception("No se encontró el carrito");
                else
                {
                    carritodto = new CarritoSesionDTO();
                    carritodetallesdto = new List<CarritoSesionDetalleDTO>();
                    foreach (var c in carrito.ListaDetalle)
                    {
                        carritodetalledto = new CarritoSesionDetalleDTO();
                        carritodetalledto.id = c.CarritoSesionDetalleId;
                        carritodetalledto.CreatedAt = c.FechaCreacion;
                        carritodetalledto.SelectedProduct = c.ProductoSeleccionado;
                        carritodetalledto.Guid = c.CarritoSesionDetalleGuid;
                        carritodetallesdto.Add(carritodetalledto);
                    }

                    carritodto.id = carrito.CarritoSesionId;
                    carritodto.Guid = carrito.CarritoSesionGuid;
                    carritodto.CreatedAt = carrito.FechaCreacion;
                    carritodto.ListDetailCart = carritodetallesdto;
                    return carritodto;
                }
            }
        }
    }
}
