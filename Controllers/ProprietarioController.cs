using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.Controllers.DTO;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.DAL.Interfaces;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.DTO;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProprietarioController : ControllerBase
    {
        private IProprietarioRepository? proprietarioRepository;
        private ITelefoneRepository? telefoneRepository;

        public ProprietarioController()
        {
            this.proprietarioRepository = new ProprietarioRepository(new ConcessionariaContext());
            this.telefoneRepository = new TelefoneRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<ProprietarioDTO> GetProprietarios()
        {
            var proprietarios = this.proprietarioRepository!.GetProprietarios();
            var telefones = this.telefoneRepository!.GetTelefones();

            var proprietariosDTO = new HashSet<ProprietarioDTO>();

            foreach (var proprietario in proprietarios)
            {
                proprietariosDTO.Add(new ProprietarioDTO
                {
                    CpfCnpj = proprietario.CpfCnpj,
                    IndicadorPessoa = proprietario.IndicadorPessoa,
                    Nome = proprietario.Nome,
                    Email = proprietario.Email,
                    Telefones = telefones.Where(t => t.ProprietarioCpfCnpj == proprietario.CpfCnpj).ToList(),
                    DataNascimento = proprietario.DataNascimento,
                    Cidade = proprietario.Cidade,
                    UF = proprietario.UF,
                    CEP = proprietario.CEP
                });
            }

            return proprietariosDTO;
        }

        [HttpGet("{cpfCnpj}")]
        public IActionResult GetProprietarioPorID(string cpfCnpj)
        {
            try
            {
                var proprietario = this.proprietarioRepository!.GetProprietarioPorCpfCnpj(cpfCnpj);

                if (proprietario is null)
                {
                    return NotFound($"Proprietário com o documento \"{cpfCnpj}\" não foi encontrado");
                }

                var telefones = this.telefoneRepository!.GetTelefones();

                var proprietarioDTO = new ProprietarioDTO
                {
                    CpfCnpj = proprietario.CpfCnpj,
                    IndicadorPessoa = proprietario.IndicadorPessoa,
                    Nome = proprietario.Nome,
                    Email = proprietario.Email,
                    Telefones = telefones.Where(t => t.ProprietarioCpfCnpj == proprietario.CpfCnpj).ToList(),
                    DataNascimento = proprietario.DataNascimento,
                    Cidade = proprietario.Cidade,
                    UF = proprietario.UF,
                    CEP = proprietario.CEP
                };

                return Ok(proprietarioDTO);
            }
            catch (NotFoundException e)
            {
                e.CriarLog();
                
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult PostProprietario([FromBody] ProprietarioForm form)
        {
            try
            {
                if (form is null)
                {
                    return BadRequest();
                }

                var proprietario = this.proprietarioRepository!.InsertProprietario(form.ToProprietario());

                return Ok(proprietario);
            }
            catch (BadRequestException e)
            {
                e.CriarLog();
                
                return BadRequest();
            }
        }

        [HttpPut("{cpfCnpj}")]
        public IActionResult UpdateProprietario(string cpfCnpj, [FromBody] ProprietarioForm form)
        {
            try
            {
                if (form is null)
                {
                    return BadRequest();
                }

                var proprietario = this.proprietarioRepository!.GetProprietarioPorCpfCnpj(cpfCnpj);

                if (proprietario is null)
                {
                    return NotFound($"Proprietário com o documento \"{cpfCnpj}\" não foi encontrado");
                }

                this.proprietarioRepository.UpdateProprietario(proprietario, form);

                proprietario = this.proprietarioRepository.GetProprietarioPorCpfCnpj(cpfCnpj);

                return Ok(proprietario);
            }
            catch (BadRequestException e)
            {
                e.CriarLog();
                
                return BadRequest();
            }
            catch (NotFoundException e)
            {
                e.CriarLog();
                
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{cpfCnpj}")]
        public IActionResult DeleteProprietario(string cpfCnpj)
        {
            try
            {
                var proprietario = this.proprietarioRepository!.GetProprietarioPorCpfCnpj(cpfCnpj);

                if (proprietario is null)
                {
                    return NotFound($"Proprietário com o documento \"{cpfCnpj}\" não foi encontrado");
                }

                this.proprietarioRepository.DeleteProprietario(cpfCnpj);

                    return Accepted();
            }
            catch (NotFoundException e)
            {
                e.CriarLog();
                
                return NotFound(e.Message);
            }
        }
    }
}
