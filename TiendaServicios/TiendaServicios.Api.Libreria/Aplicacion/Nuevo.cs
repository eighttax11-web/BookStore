using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libreria.Context;
using TiendaServicios.Api.Libreria.DTO;
using TiendaServicios.Api.Libreria.Entities;

namespace TiendaServicios.Api.Libreria.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : LibroDTO, IRequest
        {

        }

        public class EjecutaValidation : AbstractValidator<Ejecuta>
        {
            public EjecutaValidation()
            {
                RuleFor(x => x.titleBook).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoLibro context;

            public Manejador(ContextoLibro context)
            {
                this.context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new Libros
                {
                    TituloLibro = request.titleBook,
                    FechaPublicacion = request.releaseDate,
                    AutorLibro = request.autorBook,
                    LibroGuid = Guid.NewGuid().ToString()
                };

                this.context.Libros.Add(libro);

        
                var response = await this.context.SaveChangesAsync();

                return response > 0 ? Unit.Value : throw new Exception("Error al insertar");
            }
        }
    }
}

