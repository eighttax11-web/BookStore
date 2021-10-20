using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TiendaServicios.Api.Autor.Entities;
using TiendaServicios.Api.Autor.Context;
using System.Threading;
using FluentValidation;
using TiendaServicios.Api.Autor.DTO;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: AutorDTO, IRequest 
        {
            
        }

        public class EjecutaValidation: AbstractValidator<Ejecuta>
        {
            public EjecutaValidation()
            {
                RuleFor(x => x.name).NotEmpty();
                RuleFor(x => x.surname).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor context;

            public Manejador(ContextoAutor context)
            {
                this.context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.name,
                    Apellido = request.surname,
                    FechaNacimiento = request.birthDate,
                    AutorLibroGuid = Guid.NewGuid().ToString()
                };

                this.context.AutorLibro.Add(autorLibro);

                foreach(var grade in request.grade)
                {
                    var gradoAcademico = new GradoAcademico
                    {
                        Nombre = grade.name,
                        CentroAcademico = grade.academicCenter,
                        FechaGrado = grade.dateGrade,
                        AutorLibro = autorLibro,
                        GradoAcademicoGuid = Guid.NewGuid().ToString()
                    };

                    this.context.GradoAcademico.Add(gradoAcademico);
                }

                var response = await this.context.SaveChangesAsync();

                return response > 0 ? Unit.Value : throw new Exception("Error al insertar");
            }
        }
    }
}
