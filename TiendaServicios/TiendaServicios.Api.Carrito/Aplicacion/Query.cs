using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.Context;
using TiendaServicios.Api.Carrito.DTO;

namespace TiendaServicios.Api.Carrito.Aplicacion
{
    public class Query
    {
        public class ListaCarritoSesion : IRequest<List<CarritoSesionDTO>>
        {

        }

        public class Manejador : IRequestHandler<ListaCarritoSesion, List<CarritoSesionDTO>>
        {
            private readonly ContextoCarritoSesion context;
            public List<CarritoSesionDTO> carritosesiondto;
            public CarritoSesionDTO dto;
            public List<CarritoSesionDetalleDTO> carritosesiondetalledto;
            public CarritoSesionDetalleDTO detalledto;

            public Manejador(ContextoCarritoSesion context)
            {
                this.context = context;
            }
            public async Task<List<CarritoSesionDTO>> Handle(ListaCarritoSesion request, CancellationToken cancellationToken)
            {
                var carritos = await this.context.CarritoSesion.Include(detalle => detalle.ListaDetalle).ToListAsync();

                carritosesiondto = new List<CarritoSesionDTO>();

                foreach (var a in carritos)
                {
                    if (a.ListaDetalle != null)
                    {
                        carritosesiondetalledto = new List<CarritoSesionDetalleDTO>();
                        foreach (var detalle in a.ListaDetalle)
                        {
                            detalledto = new CarritoSesionDetalleDTO();
                            detalledto.id = detalle.CarritoSesionDetalleId;
                            detalledto.CreatedAt = detalle.FechaCreacion;
                            detalledto.SelectedProduct = detalle.ProductoSeleccionado;
                            detalledto.Guid = detalle.CarritoSesionDetalleGuid;
                            carritosesiondetalledto.Add(detalledto);
                        }

                        dto = new CarritoSesionDTO();
                        dto.id = a.CarritoSesionId;
                        dto.CreatedAt = a.FechaCreacion;
                        dto.Guid = a.CarritoSesionGuid;
                        dto.ListDetailCart = carritosesiondetalledto;
                        carritosesiondto.Add(dto);
                    }
                    else
                    {
                        dto = new CarritoSesionDTO();
                        dto.id = a.CarritoSesionId;
                        dto.CreatedAt = a.FechaCreacion;
                        dto.Guid = a.CarritoSesionGuid;
                        dto.ListDetailCart = null;
                        carritosesiondto.Add(dto);
                    }
                }
                return carritosesiondto;
            }
        }
    }
}
