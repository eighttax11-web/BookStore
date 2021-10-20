using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libreria.Aplicacion;
using TiendaServicios.Api.Libreria.DTO;
using TiendaServicios.Api.Libreria.Entities;

namespace TiendaServicios.Api.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMediator mediator;

        public LibroController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await this.mediator.Send(data);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<LibroDTO>>> GetAutores()
        {
            return await this.mediator.Send(new Query.ListaLibro());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Libros>> GetLibro(string id)
        {
            return await this.mediator.Send(new QueryFilter.LibroUnico() { LibroGuid = id });
        }
    }
}
