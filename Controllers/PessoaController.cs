using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private IPessoaRepository? pessoaRepository;

        public PessoaController()
        {
            this.pessoaRepository = new PessoaRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<Pessoa> GetPessoas()
        {
            return this.pessoaRepository!.GetPessoas();
        }

        [HttpGet("{id}")]
        public IActionResult GetPessoaPorID(string cpfCnpj)
        {
            var pessoa = this.pessoaRepository!.GetPessoaPorCpfCnpj(cpfCnpj);

            if (pessoa is null)
            {
                return NotFound($"Pessoa com o documento \"{cpfCnpj}\" não foi encontrada");
            }

            return Ok(pessoa);
        }
    }
}
