using FluentValidation;
using MediatR;
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
    public class Nuevo
    {
        public class Ejecuta : CarritoSesionDTO, IRequest
        {

        }

        public class EjecutaValidation : AbstractValidator<Ejecuta>
        {
            public EjecutaValidation()
            {
                
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoCarritoSesion context;

            public Manejador(ContextoCarritoSesion context)
            {
                this.context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritosesion = new CarritoSesion
                {
                    FechaCreacion = request.CreatedAt,
                    CarritoSesionGuid = Guid.NewGuid().ToString()
                };

                this.context.CarritoSesion.Add(carritosesion);

                foreach (var detalle in request.ListDetailCart)
                {
                    var carritoSesionDetalle = new CarritoSesionDetalle
                    {
                        FechaCreacion = detalle.CreatedAt,
                        ProductoSeleccionado = detalle.SelectedProduct,
                        CarritoSesion = carritosesion,
                        CarritoSesionDetalleGuid = Guid.NewGuid().ToString()
                    };

                    this.context.CarritoSesionDetalle.Add(carritoSesionDetalle);
                }

                var response = await this.context.SaveChangesAsync();

                return response > 0 ? Unit.Value : throw new Exception("Error al insertar");
            }
        }
    }
}
