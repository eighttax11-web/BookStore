using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class Query
    {
        public class ListaLibro : IRequest<List<LibroDTO>>
        {

        }

        public class Manejador : IRequestHandler<ListaLibro, List<LibroDTO>>
        {
            private readonly ContextoLibro context;
            public List<LibroDTO> librodto;
            public LibroDTO dto;
            

            public Manejador(ContextoLibro context)
            {
                this.context = context;
            }
            public async Task<List<LibroDTO>> Handle(ListaLibro request, CancellationToken cancellationToken)
            {
                var libros = await this.context.Libros.ToListAsync();

                librodto = new List<LibroDTO>();

                foreach (var a in libros)
                {
                    dto = new LibroDTO();
                    dto.id = a.LibrosId;
                    dto.titleBook = a.TituloLibro;
                    dto.releaseDate = a.FechaPublicacion;
                    dto.autorBook = a.AutorLibro;
                    dto.guid = a.LibroGuid;
                    librodto.Add(dto);   
                }

                return librodto;
            }
        }
    }
}
