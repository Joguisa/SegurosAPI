using Microsoft.AspNetCore.Mvc;
using SegurosAPI.DTOs;
using SegurosAPI.Services.Contrato;
using SegurosAPI.Services.Implementacion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SegurosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurosClientesController : ControllerBase
    {

        private readonly ISegurosClientes aseguradosService;

        public SegurosClientesController(ISegurosClientes aseguradosService)
        {
            this.aseguradosService = aseguradosService;
        }

        // GET: api/<SegurosClientesController>
        [HttpGet(Name = "GetAsegurados")]
        public async Task<ActionResult<List<SegurosClienteDTO>>> GetAsegurados()
        {
            try
            {
                var asegurados = await aseguradosService.GetSegurosClientesList();
                return asegurados;

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // GET api/<SegurosClientesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SegurosClientesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SegurosClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SegurosClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
