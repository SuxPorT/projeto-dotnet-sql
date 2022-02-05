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

            var proprietariosComTelefones = new List<Proprietario>();

            var proprietarioDTO = proprietarios.Join(
                telefones,
                proprietario => proprietario.CpfCnpj,
                telefone => telefone.ProprietarioCpfCnpj,

                (proprietario, telefone) => new ProprietarioDTO
                {
                    CpfCnpj = proprietario.CpfCnpj,
                    IndicadorPessoa = proprietario.IndicadorPessoa,
                    Nome = proprietario.Nome,
                    Email = proprietario.Email,
                    Telefones = new List<Telefone> { telefone },
                    DataNascimento = proprietario.DataNascimento,
                    Cidade = proprietario.Cidade,
                    UF = proprietario.UF,
                    CEP = proprietario.CEP
                }
            );

            return proprietarioDTO;
        }

        [HttpGet("{cpfCnpj}")]
        public IActionResult GetProprietarioPorID(string cpfCnpj)
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

        [HttpPost]
        public IActionResult PostProprietario([FromBody] ProprietarioForm form)
        {
            if (form is null)
            {
                return BadRequest();
            }

            this.proprietarioRepository!.InsertProprietario(form.ToProprietario());

            // Não necessariamente irá pegar o proprietário criado 
            // (já que ordena pelo cpfCnpj, que é uma string)
            var veiculo = this.proprietarioRepository.GetUltimoProprietario();

            return Ok(veiculo);
        }

        [HttpPut("{cpfCnpj}")]
        public IActionResult UpdateProprietario(string cpfCnpj, [FromBody] ProprietarioForm form)
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

        [HttpDelete("{cpfCnpj}")]
        public IActionResult DeleteProprietario(string cpfCnpj)
        {
            var proprietario = this.proprietarioRepository!.GetProprietarioPorCpfCnpj(cpfCnpj);

            if (proprietario is null)
            {
                return NotFound($"Proprietário com o documento \"{cpfCnpj}\" não foi encontrado");
            }

            this.proprietarioRepository.DeleteProprietario(cpfCnpj);

            return Accepted();
        }
    }
}
