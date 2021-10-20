using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Context;
using TiendaServicios.Api.Autor.Entities;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class QueryFilter
    {
        public class AutorUnico: IRequest<AutorLibro>
        {
            internal string id;

            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorLibro>
        {
            private readonly ContextoAutor context;

            public Manejador(ContextoAutor context)
            {
                this.context = context;
            }
            public async Task<AutorLibro> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await this.context.AutorLibro.Where(x => x.AutorLibroGuid.Equals(request.AutorGuid)).FirstOrDefaultAsync();

                if (autor == null)
                    throw new Exception("No se encontró el autor");
                else
                    return autor;
            }
        }
    }
}
