using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.Aplicacion;
using TiendaServicios.Api.Carrito.DTO;
using TiendaServicios.Api.Carrito.Entities;

namespace TiendaServicios.Api.Carrito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly IMediator mediator;

        public CarritoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await this.mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<CarritoSesionDTO>>> GetCarritos()
        {
            return await this.mediator.Send(new Query.ListaCarritoSesion());
        }

        /*[HttpGet("{id}")]
        public async Task<ActionResult<CarritoSesionDTO>> GetCarrito(string id)
        {
            return await this.mediator.Send(new QueryFilter.CarritoUnico() { CarritoGuid = id });
        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDTO>> GetCarrito(int id)
        {
            return await this.mediator.Send(new QueryCarrito.Ejecuta { carritoSesionId = id });
        }
    }
}
