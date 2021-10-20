using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Context;
using TiendaServicios.Api.Autor.DTO;
using TiendaServicios.Api.Autor.Entities;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Query
    {
        public class ListaAutor : IRequest<List<AutorDTO>>
        {

        }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorDTO>>
        {
            private readonly ContextoAutor context;
            public List<AutorDTO> autoresdto;
            public AutorDTO dto;
            public List<GradoDTO> gradosdto;
            public GradoDTO gradedto;

            public Manejador(ContextoAutor context)
            {
                this.context = context;
            }
            public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await this.context.AutorLibro.Include(grade => grade.ListaGradoAcademico).ToListAsync();

                autoresdto = new List<AutorDTO>();

                foreach (var a in autores)
                {
                    if(a.ListaGradoAcademico != null)
                    {
                        gradosdto = new List<GradoDTO>();
                        foreach(var grade in a.ListaGradoAcademico)
                        {
                            gradedto = new GradoDTO();
                            gradedto.id = grade.GradoAcademicoId;
                            gradedto.name = grade.Nombre;
                            gradedto.academicCenter = grade.CentroAcademico;
                            gradedto.dateGrade = grade.FechaGrado;
                            gradedto.guid = grade.GradoAcademicoGuid;
                            gradosdto.Add(gradedto);
                        }

                        dto = new AutorDTO();
                        dto.id = a.AutorLibroId;
                        dto.name = a.Nombre;
                        dto.surname = a.Apellido;
                        dto.birthDate = a.FechaNacimiento;
                        dto.guid = a.AutorLibroGuid;
                        dto.grade = gradosdto;
                        autoresdto.Add(dto);
                    } else
                    {
                        dto = new AutorDTO();
                        dto.id = a.AutorLibroId;
                        dto.name = a.Nombre;
                        dto.surname = a.Apellido;
                        dto.birthDate = a.FechaNacimiento;
                        dto.guid = a.AutorLibroGuid;
                        dto.grade = null;
                        autoresdto.Add(dto);
                    }
                }

                /* var query = await this.context.AutorLibro
                    .Join(
                        
                        this.context.GradoAcademico,
                        autor => autor.AutorLibroId,
                        grado => grado.AutorLibro.AutorLibroId,
                        (autor, grado) => new
                        {
                            NombreAutor = autor.Nombre,
                            GradoAcademico = grado.Nombre
                        }
                    ).ToListAsync();*/


                return autoresdto;
            }
        }
    }
}
