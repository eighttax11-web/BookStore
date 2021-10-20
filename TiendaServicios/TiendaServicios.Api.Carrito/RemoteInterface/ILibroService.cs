using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.RemoteModel;

namespace TiendaServicios.Api.Carrito.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool resultado, LibroRemote libro, string errorMessage)> getLibro(Guid LibroId);
    }
}
