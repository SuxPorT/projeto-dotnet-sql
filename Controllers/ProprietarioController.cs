using CustomExceptions;
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
            var concessionariaContext = new ConcessionariaContext();

            this.proprietarioRepository = new ProprietarioRepository(concessionariaContext);
            this.telefoneRepository = new TelefoneRepository(concessionariaContext);
        }

        [HttpGet]
        public IEnumerable<ProprietarioDTO> GetProprietarios()
        {
            try
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
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Proprietario).Name, e);

                return new HashSet<ProprietarioDTO>();
            }
        }

        [HttpGet("{cpfCnpj}")]
        public IActionResult GetProprietarioPorID(string cpfCnpj)
        {
            try
            {
                var proprietario = this.proprietarioRepository!.GetProprietarioPorCpfCnpj(cpfCnpj);

                if (proprietario is null)
                {
                    throw new NotFoundException(typeof(Proprietario).Name, $"Proprietário com o documento \"{cpfCnpj}\" não foi encontrado");
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
                LogException.CriarLog(typeof(Proprietario).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Proprietario).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpPost]
        public IActionResult PostProprietario([FromBody] ProprietarioForm form)
        {
            try
            {
                if (form is null)
                {
                    throw new BadRequestException(typeof(ProprietarioForm).Name, "O formulário está inválido");
                }

                var proprietario = this.proprietarioRepository!.InsertProprietario(form.ToProprietario());

                return Ok(proprietario);
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(ProprietarioForm).Name, e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(ProprietarioForm).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpPut("{cpfCnpj}")]
        public IActionResult UpdateProprietario(string cpfCnpj, [FromBody] ProprietarioForm form)
        {
            try
            {
                var proprietario = this.proprietarioRepository!.GetProprietarioPorCpfCnpj(cpfCnpj);

                if (proprietario is null)
                {
                    throw new NotFoundException(typeof(Proprietario).Name, $"Proprietário com o documento \"{cpfCnpj}\" não foi encontrado");
                }

                if (form is null)
                {
                    throw new BadRequestException(typeof(ProprietarioForm).Name, "O formulário está inválido");
                }

                this.proprietarioRepository.UpdateProprietario(proprietario, form);

                proprietario = this.proprietarioRepository.GetProprietarioPorCpfCnpj(cpfCnpj);

                return Ok(proprietario);
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Proprietario).Name, e);

                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(ProprietarioForm).Name, e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Telefone).Name, e);

                return BadRequest("Erro não especificado");
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
                    throw new NotFoundException(typeof(Proprietario).Name, $"Proprietário com o documento \"{cpfCnpj}\" não foi encontrado");
                }

                this.proprietarioRepository.DeleteProprietario(cpfCnpj);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Proprietario).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Proprietario).Name, e);

                return BadRequest("Erro não especificado");
            }
        }
    }
}
