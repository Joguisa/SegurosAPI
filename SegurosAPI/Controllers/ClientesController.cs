using Microsoft.AspNetCore.Mvc;
using SegurosAPI.DTOs;
using SegurosAPI.Services.Contrato;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SegurosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ICliente _clienteService;

        public ClientesController(ICliente clienteService)
        {
            this._clienteService = clienteService;
        }


        // GET: api/<ClientesController>
        [HttpGet(Name = "ListarClientes")]
        public async Task<ActionResult<List<ClienteDto>>> ListarClientes()
        {
            try
            {
                var clientes = await _clienteService.GetList();
                return clientes;

            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}", Name = "ObtenerCliente")]
        public async Task<ActionResult<ClienteDto>> ObtenerCliente(int id)
        {
            try
            {
                var cliente = await _clienteService.Get(id);
                return cliente != null
                    ? Ok(cliente)
                    : NotFound($"No existe un cliente con ese id: {id}");
            } catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ClientesController>
        [HttpPost(Name = "PostCliente")]
        public async Task<ActionResult> PostCliente([FromBody] ClienteDto clienteDto)
        {
            try
            {
                await _clienteService.Add(clienteDto);
                return Ok("Cliente agregado con éxito");
            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}", Name = "PutCliente")]
        public async Task<ActionResult> PutCliente(int id, [FromBody] ClienteDto clienteDto)
        {
            try
            {
                await _clienteService.Update(clienteDto, id);
                return NoContent();

            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}", Name = "DeleteCliente")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            try
            {
                await _clienteService.Delete(id);
                return NoContent();

            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
