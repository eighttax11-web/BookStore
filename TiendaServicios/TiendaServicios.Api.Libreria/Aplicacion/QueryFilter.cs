using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libreria.Context;
using TiendaServicios.Api.Libreria.Entities;

namespace TiendaServicios.Api.Libreria.Aplicacion
{
    public class QueryFilter
    {
        public class LibroUnico : IRequest<Libros>
        {
            internal string id;

            public string LibroGuid { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, Libros>
        {
            private readonly ContextoLibro context;

            public Manejador(ContextoLibro context)
            {
                this.context = context;
            }
            public async Task<Libros> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var autor = await this.context.Libros.Where(x => x.LibroGuid.Equals(request.LibroGuid)).FirstOrDefaultAsync();

                if (autor == null)
                    throw new Exception("No se encontró el autor");
                else
                    return autor;
            }
        }
    }
}
