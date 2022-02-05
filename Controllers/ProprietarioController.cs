using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProprietarioController : ControllerBase
    {
        private IProprietarioRepository? proprietarioRepository;

        public ProprietarioController()
        {
            this.proprietarioRepository = new ProprietarioRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<Proprietario> GetProprietarios()
        {
            return this.proprietarioRepository!.GetProprietarios();
        }

        [HttpGet("{id}")]
        public IActionResult GetPessoaPorID(string cpfCnpj)
        {
            var proprietario = this.proprietarioRepository!.GetProprietarioPorCpfCnpj(cpfCnpj);

            if (proprietario is null)
            {
                return NotFound($"Proprietário com o documento \"{cpfCnpj}\" não foi encontrado");
            }

            return Ok(proprietario);
        }
    }
}
