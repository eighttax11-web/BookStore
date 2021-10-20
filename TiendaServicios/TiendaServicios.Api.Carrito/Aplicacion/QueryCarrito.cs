using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.Context;
using TiendaServicios.Api.Carrito.RemoteInterface;

namespace TiendaServicios.Api.Carrito.Aplicacion
{
    public class QueryCarrito
    {
        public class Ejecuta: IRequest<CarritoDTO>
        {
            public int carritoSesionId { get; set; }


        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly ContextoCarritoSesion context;
            private readonly ILibroService libroService;

            public Manejador(ContextoCarritoSesion context, ILibroService libroService)
            {
                this.context = context;
                this.libroService = libroService;
            }

            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await context.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.carritoSesionId);
                var carritoSesionDetalle = await context.CarritoSesionDetalle.Where(x => x.CarritoSesion.CarritoSesionId == request.carritoSesionId).ToListAsync();

                var listaCarritoDTO = new List<CarritoDetalleDTO>();

                foreach(var libro in carritoSesionDetalle)
                {
                    var response = await libroService.getLibro(new Guid(libro.ProductoSeleccionado));

                    if (response.resultado)
                    {
                        var objetoLibro = response.libro;
                        var carritoDetalle = new CarritoDetalleDTO {
                            TituloLibro = objetoLibro.TituloLibro,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = new Guid(objetoLibro.LibroGuid)
                        };

                        listaCarritoDTO.Add(carritoDetalle);
                    }
                }

                var carritoSesionDTO = new CarritoDTO
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDTO
                };

                return carritoSesionDTO;
            }
        }
    }
}
