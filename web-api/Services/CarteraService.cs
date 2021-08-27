using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_api.Data.AppDb.Context;
using web_api.Data.AppDb.Model;

namespace web_api.Services
{
    public interface ICarteraService
    {
        Task<List<ConsCartRest>> ConsultarCarteraAsync(
            int? clienteId,
            string nombre,
            string apellidos,
            int? cuentaId,
            string numCuenta,
            string nomProducto
        );
    }

    public class CarteraService : ICarteraService
    {
        private readonly AppdbContext _appdbContext;

        public CarteraService(AppdbContext appdbContenxt)
        {
            _appdbContext = appdbContenxt;
        }

        public async Task<List<ConsCartRest>> ConsultarCarteraAsync(int? clienteId, string nombre, string apellidos, int? cuentaId, string numCuenta, string nomProducto)
        {

            IQueryable<ConsCartRest> query = from ca in _appdbContext.Cartera
                                             join cu in _appdbContext.Cuenta on ca.CuentaId equals cu.CuentaId
                                             join cl in _appdbContext.Cliente on cu.ClienteId equals cl.ClienteId
                                             join pr in _appdbContext.Producto on ca.ProductoId equals pr.ProductoId
                                             where cl.ClienteId == (clienteId ?? cl.ClienteId)
                                             && cl.Nombre == (nombre ?? cl.Nombre)
                                             && cl.Apellidos == (apellidos ?? cl.Apellidos)
                                             && cu.CuentaId == (cuentaId ?? cu.CuentaId)
                                             && cu.Numero == (numCuenta ?? cu.Numero)
                                             && pr.Nombre == (nomProducto ?? pr.Nombre)
                                             select new ConsCartRest
                                             {
                                                 Nombre = cl.Nombre,
                                                 Apellidos = cl.Apellidos,
                                                 Cuenta = cu.Numero,
                                                 Producto = pr.Nombre
                                             };

            return await query.ToListAsync();
        }
    }

    public class ConsCartRest
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Cuenta { get; set; }
        public int Cantidad { get; set; }
        public string Producto { get; set; }
    }
}
