using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PruebaApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            string[] datos = new string[] { "hola", "adios" };
            return Ok(datos);
        }
        [HttpGet("numero")]
        public IActionResult Getnumero()
        {
            return Ok(100);
        }
        [HttpGet("{nombre}")]
        public IActionResult Saludar(string nombre)
        {
            return Ok("hola "+ nombre);
        }
        [HttpPost]
        public IActionResult Post(int numero)
        {
            return Ok(numero * numero);
        }
    }
}
