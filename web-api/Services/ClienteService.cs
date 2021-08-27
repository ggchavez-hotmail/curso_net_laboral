using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_api.Data.AppDb.Context;
using web_api.Data.AppDb.Model;

namespace web_api.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> ConsultarClienteAsync(int? clienteId, string nombre, string apellidos);
    }

    public class ClienteService : IClienteService
    {
        private readonly AppdbContext _appdbContext;

        public ClienteService(AppdbContext appdbContenxt)
        {
            _appdbContext = appdbContenxt;
        }

        public async Task<List<Cliente>> ConsultarClienteAsync(int? clienteId, string nombre, string apellidos)
        {
            IQueryable<Cliente> query = from c in _appdbContext.Cliente
                                        where c.ClienteId == (clienteId ?? c.ClienteId)
                                        && c.Nombre == (nombre ?? c.Nombre)
                                        && c.Apellidos == (apellidos ?? c.Apellidos)
                                        select c;

            return await query.ToListAsync();
        }
    }
}
