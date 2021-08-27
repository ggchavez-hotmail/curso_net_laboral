using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_api.Data.AppDb.Context;
using web_api.Data.AppDb.Model;

namespace web_api.Services
{
    public interface ITransaccionService
    {
        Task ComprarAsync(int productoId, int cuentaId, int cantidad);

        Task VenderAsync(int productoId, int cuentaId, int cantidad);
    }
    public class TransaccionService : ITransaccionService
    {
        private readonly AppdbContext _appdbContext;

        private readonly ICalculoService _calculoService;
        public TransaccionService(AppdbContext appdbContext, ICalculoService calculoService)
        {
            _appdbContext = appdbContext;
            _calculoService = calculoService;
        }

        public async Task ComprarAsync(int productoId, int cuentaId, int cantidad)
        {
            var productoExistente = await _appdbContext.Cartera
                                    .Where(c => c.ProductoId == productoId && c.CuentaId == cuentaId)
                                    .FirstOrDefaultAsync();
            if (productoExistente == null)
            {
                decimal montoTotal = await CalcularMontoAsync(productoId, cantidad);

                Cartera nuevoProducto = new Cartera()
                {
                    ProductoId = productoId,
                    CuentaId = cuentaId,
                    Cantidad = cantidad,
                    MontoTotal = montoTotal
                };
                _appdbContext.Cartera.Add(nuevoProducto);
                await _appdbContext.SaveChangesAsync();
            }
            else
            {
                await ActualizarCartera(productoExistente, cantidad, productoId);
            }
        }

        public async Task VenderAsync(int productoId, int cuentaId, int cantidad)
        {
            var productoExistente = await _appdbContext.Cartera
                                    .Where(c => c.ProductoId == productoId && c.CuentaId == cuentaId)
                                    .FirstOrDefaultAsync();

            if (productoExistente == null)
            {
                string message = $"No hay producto con el id '{productoId}' para vender'";
                throw new ArgumentException(message, nameof(productoId));
            }

            if (productoExistente.Cantidad < cantidad)
            {
                string message = $"La cantidad a vender '{cantidad}' es mayor que la cantidad disponible '{productoExistente.Cantidad}' para vender'";
                throw new ArgumentException(message, nameof(productoId));
            }
            else
            {
                if (cantidad == productoExistente.Cantidad)
                {
                    _appdbContext.Remove(productoExistente);
                    await _appdbContext.SaveChangesAsync();
                }
                else
                {
                    await ActualizarCartera(productoExistente, -cantidad, productoId);
                }
            }
        }
        public async Task ActualizarCartera(Cartera item, int cantidad, int productoId)
        {
            int cantidadActualizada = item.Cantidad + cantidad;
            decimal montoTotal = await CalcularMontoAsync(productoId, cantidad);

            item.Cantidad = cantidadActualizada;
            item.MontoTotal = montoTotal;

            await _appdbContext.SaveChangesAsync();
        }

        public async Task<decimal> CalcularMontoAsync(int productoId, int cantidad)
        {
            decimal precio = await ObtenerPrecioAsync(productoId);

            return _calculoService.PxQ(precio, cantidad);
        }

        public async Task<decimal> ObtenerPrecioAsync(int productoId)
        {
            return await _appdbContext.Precio
                    .Where(x => x.ProductoId == productoId)
                    .Select(x => x.Valor)
                    .FirstOrDefaultAsync();
        }
    }
}
