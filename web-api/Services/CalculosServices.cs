namespace web_api.Services
{
    public interface ICalculoService
    {
        decimal PxQ(decimal precio, int cantidad);
    }

    public class CalculoService : ICalculoService
    {
        public decimal PxQ(decimal precio, int cantidad) => precio * cantidad;
    }
}
