using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.Entities;

namespace TiendaServicios.Api.Carrito.Context
{
    public class ContextoCarritoSesion: DbContext
    {
        public ContextoCarritoSesion(DbContextOptions<ContextoCarritoSesion> options) : base(options)
        {

        }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}
