using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEscola_API.Data;
using ProjetoEscola_API.Models;


namespace ProjetoEscola_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcessionariaController : Controller
    {
        private readonly ConcesissionariaContext _context;
        public ConcessionariaController(ConcesissionariaContext context)
        {
            // construtor
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<Concessionaria>> GetAll()
        {
            return _context.Concessionaria.ToList();
        }

        [HttpGet("{ConcessionariaId}")]
        public ActionResult<List<Concessionaria>> Get(int ConcessionariaId)
        {
            try
            {
                var result = _context.Concessionaria.Find(ConcessionariaId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
        [HttpPost]
        public async Task<ActionResult> post(Concessionaria model)
        {
            try
            {
                _context.Concessionaria.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/concessionaria/{model.codLoja}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();
        }


        [HttpPut("{ConcessionariaId}")]
        public async Task<IActionResult> put(int ConcessionariaId, Concessionaria ConcessionariaAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.Concessionaria.FindAsync(ConcessionariaId);
                if (ConcessionariaId != result.id)
                {
                    return BadRequest();
                }
                result.codLoja = ConcessionariaAlt.codLoja;
                result.nomeLoja = ConcessionariaAlt.nomeLoja;
                result.cep = ConcessionariaAlt.cep;
                result.endereco = ConcessionariaAlt.endereco;
                result.estado = ConcessionariaAlt.estado;
                await _context.SaveChangesAsync();
                return Created($"/api/concessionaria/{ConcessionariaAlt.codLoja}", ConcessionariaAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }


        [HttpDelete("{ConcessionariaId}")]
        public async Task<ActionResult> delete(int ConcessionariaId)
        {
            try
            {
                //verifica se existe veiculo a ser excluído
                var concessionaria = await _context.Concessionaria.FindAsync(ConcessionariaId);
                if (concessionaria == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(concessionaria);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falhano acesso ao banco de dados.");
            }
        }
    }
}