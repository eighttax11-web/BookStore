using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libreria.Entities;

namespace TiendaServicios.Api.Libreria.Context
{
    public class ContextoLibro: DbContext
    {
        public ContextoLibro(DbContextOptions<ContextoLibro> options) : base(options)
        {

        }

        public DbSet<Libros> Libros { get; set; }
    }
}
