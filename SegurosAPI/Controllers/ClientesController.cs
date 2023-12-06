using Microsoft.AspNetCore.Mvc;
using SegurosAPI.DTOs;
using SegurosAPI.Models;
using SegurosAPI.Services.Contrato;
using SegurosAPI.Services.Implementacion;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SegurosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ICliente clienteService;

        public ClientesController(ICliente clienteService)
        {
            this.clienteService = clienteService;
        }


        // GET: api/<ClientesController>
        [HttpGet(Name = "listarClientes")]
        public async Task<ActionResult<List<ClienteDTO>>> listarClientes()
        {
            try
            {
                var clientes = await clienteService.GetList();
                return clientes;

            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}", Name = "ObtenerCliente")]
        public async Task<ActionResult<ClienteDTO>> ObtenerCliente(int id)
        {
            try
            {
                var cliente = await clienteService.Get(id);
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
        public async Task<ActionResult> PostCliente([FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                await clienteService.Add(clienteDTO);
                return Ok("Cliente agregado con éxito");
            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}", Name = "PutCliente")]
        public async Task<ActionResult> PutCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                await clienteService.Update(clienteDTO, id);
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
                await clienteService.Delete(id);
                return NoContent();

            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
