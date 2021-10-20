using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.Carrito.RemoteInterface;
using TiendaServicios.Api.Carrito.RemoteModel;

namespace TiendaServicios.Api.Carrito.RemoteService
{
    public class LibroService : ILibroService
    {
        private readonly IHttpClientFactory httpClient;

        private readonly ILogger<LibroService> logger;

        public LibroService(IHttpClientFactory httpClient, ILogger<LibroService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<(bool resultado, LibroRemote libro, string errorMessage)> getLibro(Guid LibroId)
        {
            try
            {
                var cliente = httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/Libro/{LibroId}");

                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);

                    return (true, resultado, "");
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());

                return (false, null, e.Message);
            }
        }
    }
}
