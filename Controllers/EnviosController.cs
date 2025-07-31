using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Repository;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        private readonly IEnvioRepository _repository;

        public EnviosController(IEnvioRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] string dni, string domicilio)// es  FromQuery porque hay que mandar esos parametros que van a ir en la url
        {
            try
            {
                return Ok(_repository.GetEnvios(dni, domicilio));
            }
            catch (Exception)
            {

                return StatusCode(500, "error interno");
            }
        }
        [HttpPost("envios")] 

        public IActionResult Post([FromBody] Envio e) // es FromBody por que se debe mandar todo el cuerpo del objeto, para hacer la insercion
        {
            if (IsValid(e))
            {
                _repository.Create(e);
                return Ok("Envio registrado con exito!");
            }
            else
            {
                return BadRequest("Los Datos ingresados no son validos");
            }

        }
        private bool IsValid(Envio e)
        {
            return !string.IsNullOrEmpty(e.Direccion) && !string.IsNullOrEmpty(e.PalabraSecreta) && e.Fecha != DateTime.MinValue;
                
        }

    }
}
