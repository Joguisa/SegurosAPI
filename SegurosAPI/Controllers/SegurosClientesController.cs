using Microsoft.AspNetCore.Mvc;
using SegurosAPI.DTOs;
using SegurosAPI.Services.Contrato;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SegurosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurosClientesController : ControllerBase
    {

        private readonly ISegurosClientes _aseguradosService;

        public SegurosClientesController(ISegurosClientes aseguradosService)
        {
            this._aseguradosService = aseguradosService;
        }

        // GET: api/<SegurosClientesController>
        [HttpGet(Name = "GetAsegurados")]
        public async Task<ActionResult<List<SegurosClienteDto>>> GetAsegurados()
        {
            try
            {
                var asegurados = await _aseguradosService.GetSegurosClientesList();
                return Ok(asegurados);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<SegurosClientesController>/5
        [HttpGet("{id}", Name = "GetAseguradosById")]
        public async Task<ActionResult<SegurosClienteDto>> GetAseguradosById(int id)
        {
            try
            {
                var asegurados = await _aseguradosService.GetSegurosCliente(id);
                return asegurados != null
                    ? Ok(asegurados)
                    : NotFound($"No existe un asegurado con ese id: {id}");
            } catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "AddSegurosCliente")]
        public async Task<ActionResult<SegurosClienteDto>> AddSegurosCliente([FromBody] CrearAseguradoDto modelo)
        {
            try
            {
                var asegurados = await _aseguradosService.AddSegurosCliente(modelo);
                return Ok(asegurados);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // PUT api/<SegurosClientesController>/5
        [HttpPut("{id}", Name = "PutAsegurado")]
        public async Task<ActionResult> PutAsegurado([FromBody] CrearAseguradoDto modelo, int id)
        {
            try
            {
                await _aseguradosService.UpdateSegurosCliente(modelo, id);
                return NoContent();

            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<SegurosClientesController>/5
        [HttpDelete("{id}", Name = "DeleteAsegurado")]
        public async Task<ActionResult> DeleteAsegurado(int id)
        {
            try
            {
                await _aseguradosService.DeleteSegurosCliente(id);
                return NoContent();
            } catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
