using CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.DAL.Interfaces;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.DTO;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelefoneController : ControllerBase
    {
        private ITelefoneRepository? telefoneRepository;
        private IProprietarioRepository? proprietarioRepository;

        public TelefoneController()
        {
            this.telefoneRepository = new TelefoneRepository(new ConcessionariaContext());
            this.proprietarioRepository = new ProprietarioRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<TelefoneDTO> GetTelefones()
        {
            try
            {
                var telefones = this.telefoneRepository!.GetTelefones();
                var proprietarios = this.proprietarioRepository!.GetProprietarios();

                var telefonesDTO = (
                    from telefone in telefones
                    join proprietario in proprietarios
                    on telefone.ProprietarioCpfCnpj equals proprietario.CpfCnpj

                    select new TelefoneDTO
                    {
                        TelefoneId = telefone.TelefoneId,
                        Proprietario = proprietario,
                        Codigo = telefone.Codigo
                    }
                );

                return telefonesDTO;
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Telefone).Name, e);

                return new HashSet<TelefoneDTO>();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTelefonePorID(int id)
        {
            try
            {
                var telefone = this.telefoneRepository!.GetTelefonePorID(id);

                if (telefone is null)
                {
                    throw new NotFoundException(typeof(Telefone).Name, $"Telefone com o id \"{id}\" não foi encontrado");
                }

                return Ok(telefone);
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Telefone).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Telefone).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpPost]
        public IActionResult PostTelefone([FromBody] TelefoneForm form)
        {
            try
            {
                if (form is null)
                {
                    throw new BadRequestException(typeof(TelefoneForm).Name, "O formulário está inválido");
                }

                this.telefoneRepository!.InsertTelefone(form.ToTelefone());

                var telefone = this.telefoneRepository.GetUltimoTelefone();

                return Ok(telefone);
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(TelefoneForm).Name, e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(TelefoneForm).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTelefone(int id, [FromBody] TelefoneForm form)
        {
            try
            {
                var telefone = this.telefoneRepository!.GetTelefonePorID(id);

                if (telefone is null)
                {
                    throw new NotFoundException(typeof(Telefone).Name, $"Telefone com o id \"{id}\" não foi encontrado");
                }

                if (form is null)
                {
                    throw new BadRequestException(typeof(TelefoneForm).Name, "O formulário está inválido");
                }

                this.telefoneRepository.UpdateTelefone(telefone, form);

                telefone = this.telefoneRepository.GetTelefonePorID(id);

                return Ok(telefone);
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Telefone).Name, e);

                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(TelefoneForm).Name, e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Telefone).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTelefone(int id)
        {
            try
            {
                var telefone = this.telefoneRepository!.GetTelefonePorID(id);

                if (telefone is null)
                {
                    throw new NotFoundException(typeof(Telefone).Name, $"Telefone com o id \"{id}\" não foi encontrado");
                }

                this.telefoneRepository.DeleteTelefone(id);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Telefone).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Telefone).Name, e);

                return BadRequest("Erro não especificado");
            }
        }
    }
}
