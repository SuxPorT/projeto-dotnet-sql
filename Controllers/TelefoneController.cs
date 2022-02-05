using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.DAL.Interfaces;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

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

        [HttpPost]
        public IActionResult PostTelefone([FromBody] TelefoneForm form)
        {
            if (form is null)
            {
                return BadRequest();
            }

            this.telefoneRepository!.InsertTelefone(form.ToTelefone());

            var telefone = this.telefoneRepository.GetUltimoTelefone();

            return Ok(telefone);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTelefone(int id, [FromBody] TelefoneForm form)
        {
            if (form is null)
            {
                return BadRequest();
            }

            var telefone = this.telefoneRepository!.GetTelefonePorID(id);

            if (telefone is null)
            {
                return NotFound($"Telefone com o id \"{id}\" não foi encontrado");
            }

            this.telefoneRepository.UpdateTelefone(telefone, form);

            telefone = this.telefoneRepository.GetTelefonePorID(id);

            return Ok(telefone);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTelefone(int id)
        {
            var telefone = this.telefoneRepository!.GetTelefonePorID(id);

            if (telefone is null)
            {
                return NotFound($"Telefone com o id \"{id}\" não foi encontrado");
            }

            this.telefoneRepository.DeleteTelefone(id);

            return Accepted();
        }
    }
}
