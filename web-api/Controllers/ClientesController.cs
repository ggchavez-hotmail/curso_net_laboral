using Microsoft.AspNetCore.Mvc;
using Model.Cliente;
namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesControllers : ControllerBase
    {
        [HttpGet]
        public ActionResult ObtenerClientes([FromQuery] int limite)
        {
            Cliente[] clientes = {
                new() {Nombre = "Juan", Apellidos= "Martinez", Direccion= "Calle A N° 1234", Telefono = "+56 9 2728 1232"},
                new() {Nombre = "Pepe", Apellidos= "Martinez", Direccion= "Calle A N° 1234", Telefono = "+56 9 2728 1232"},
                new() {Nombre = "Ana", Apellidos= "Martinez", Direccion= "Calle A N° 1234", Telefono = "+56 9 2728 1232"},
                new() {Nombre = "Francisca", Apellidos= "Martinez", Direccion= "Calle A N° 1234", Telefono = "+56 9 2728 1232"},
            } ;

            if(!clientes.Any())
                return NotFound();

            return Ok(clientes.Take(limite));
        }

        [HttpPost]
        public ActionResult CrearCliente([FromBody] string nombre)
        {
            bool ocurrioAlgomalo = false;
            if(ocurrioAlgomalo)
                return BadRequest();
            
            return Created("", nombre);

        }

        // /api/Clientes/1
        [HttpDelete("{id}")]
        public ActionResult BorrarCliente(string id)
        {
            bool ocurrioAlgomalo = false;
            if(ocurrioAlgomalo)
                return BadRequest();

            return NoContent();
        }
    }
}
