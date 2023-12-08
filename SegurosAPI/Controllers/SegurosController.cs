using Microsoft.AspNetCore.Mvc;
using SegurosAPI.DTOs;
using SegurosAPI.Services.Contrato;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SegurosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurosController : ControllerBase
    {
        private readonly ISeguro _seguroService;

        public SegurosController(ISeguro seguroService)
        {
            this._seguroService = seguroService;
        }


        // GET: api/<SegurosController>
        [HttpGet(Name = "ListarSeguros" )]
        public async Task<ActionResult<List<SeguroDto>>> ListarSeguros()
        {
            try
            {
                var seguros = await _seguroService.GetList();
                return seguros;
            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/<SegurosController>/5
        [HttpGet("{id}", Name = "GetSeguro")]
        public async Task<ActionResult<SeguroDto>> GetSeguro(int id)
        {
            try
            {
                var seguro = await _seguroService.Get(id);
                return Ok(seguro);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<SegurosController>
        [HttpPost(Name = "PostSeguro")]
        public async Task<ActionResult> PostSeguro([FromBody] SeguroDto seguroDto)
        {
            try
            {
                await _seguroService.Add(seguroDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SegurosController>/5
        [HttpPut("{id}", Name = "PutSeguro")]
        public async Task<ActionResult> PutSeguro(int id, [FromBody] SeguroDto seguroDto)
        {
            try
            {
                await _seguroService.Update(seguroDto, id);
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
                await _seguroService.Delete(id);
                return NoContent();
            } catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
