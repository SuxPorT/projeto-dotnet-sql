using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelefoneController : ControllerBase
    {
        private ITelefoneRepository? telefoneRepository;

        public TelefoneController()
        {
            this.telefoneRepository = new TelefoneRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<Telefone> GetTelefones()
        {
            return this.telefoneRepository!.GetTelefones();
        }

        [HttpGet("{id}")]
        public IActionResult GetTelefonePorID(int id)
        {
            var telefone = this.telefoneRepository!.GetTelefonePorID(id);

            if (telefone is null)
            {
                return NotFound($"Telefone com o id \"{id}\" não foi encontrado");
            }

            return Ok(telefone);
        }
    }
}
