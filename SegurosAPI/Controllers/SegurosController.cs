using Microsoft.AspNetCore.Mvc;
using SegurosAPI.DTOs;
using SegurosAPI.Services.Contrato;
using SegurosAPI.Services.Implementacion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SegurosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurosController : ControllerBase
    {
        private readonly ISeguro seguroService;

        public SegurosController(ISeguro seguroService)
        {
            this.seguroService = seguroService;
        }


        // GET: api/<SegurosController>
        [HttpGet(Name = "listarSeguros" )]
        public async Task<ActionResult<List<SeguroDTO>>> listarSeguros()
        {
            try
            {
                var seguros = await seguroService.GetList();
                return seguros;
            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/<SegurosController>/5
        [HttpGet("{id}", Name = "GetSeguro")]
        public async Task<ActionResult<SeguroDTO>> GetSeguro(int id)
        {
            try
            {
                var seguro = await seguroService.Get(id);
                return seguro != null
                    ? Ok(seguro)
                    : NotFound($"No existe un seguro con ese id: {id}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<SegurosController>
        [HttpPost(Name = "PostSeguro")]
        public async Task<ActionResult> PostSeguro([FromBody] SeguroDTO seguroDTO)
        {
            try
            {
                await seguroService.Add(seguroDTO);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SegurosController>/5
        [HttpPut("{id}", Name = "PutSeguro")]
        public async Task<ActionResult> PutSeguro(int id, [FromBody] SeguroDTO seguroDTO)
        {
            try
            {
                await seguroService.Update(seguroDTO, id);
                return NoContent();
            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<SegurosController>/5
        [HttpDelete("{id}", Name = "DeleteSeguro")]
        public async Task<ActionResult> DeleteSeguro(int id)
        {
            try
            {
                await seguroService.Delete(id);
                return NoContent();
            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
