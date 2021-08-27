using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api.Data.AppDb.Model;
using web_api.Services;

namespace web_api.Controllers.Negocio
{
    [Route("api/negocio/[controller]")]
    [ApiController]

    public class ReportesController : ControllerBase
    {
        private readonly ICarteraService _carteraService;

        private readonly IClienteService _clienteService;

        public ReportesController(ICarteraService carteraService, IClienteService clienteService)
        {
            _carteraService = carteraService;
            _clienteService = clienteService;
        }

        // GET: /api/negocio/Reportes/Cartera
        [HttpGet("Cartera")]
        public async Task<List<ConsCartRest>> ConsultarCartera([FromQuery] ConsultaCarteraParams param)
        {
            return await _carteraService.ConsultarCarteraAsync(
                param.ClienteId, param.Nombre, param.Apellidos, param.CuentaId, param.NumCuenta, param.NomProducto
            );
        }

        // GET: /api/negocio/Reportes/Cliente
        [HttpGet("Cliente")]
        public async Task<List<Cliente>> ConsultarCliente([FromQuery] ConsultaClienteParams param)
        {
            return await _clienteService.ConsultarClienteAsync(
                param.ClienteId, param.Nombre, param.Apellidos
            );
        }

        public class ConsultaCarteraParams
        {
            public int? ClienteId { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public int? CuentaId { get; set; }
            public string NumCuenta { get; set; }
            public string NomProducto { get; set; }
        }

        public class ConsultaClienteParams
        {
            public int? ClienteId { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
        }
    }

}
