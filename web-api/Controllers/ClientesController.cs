using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesControllers : ControllerBase
    {
        [HttpGet]
        public ActionResult ObtenerClientes([FromQuery] int limite)
        {
            string[] clientes = {"Juan", "Pepe"};
            
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
