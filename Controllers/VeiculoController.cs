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
    public class VeiculoController : ControllerBase
    {
        private IVeiculoRepository? veiculoRepository;
        private IProprietarioRepository? proprietarioRepository;
        private ITelefoneRepository? telefoneRepository;
        private IAcessorioRepository? acessorioRepository;

        public VeiculoController()
        {
            this.veiculoRepository = new VeiculoRepository(new ConcessionariaContext());
            this.proprietarioRepository = new ProprietarioRepository(new ConcessionariaContext());
            this.telefoneRepository = new TelefoneRepository(new ConcessionariaContext());
            this.acessorioRepository = new AcessorioRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<VeiculoDTO> GetVeiculos()
        {
            try
            {
                var veiculos = this.veiculoRepository!.GetVeiculos();
                var proprietarios = this.proprietarioRepository!.GetProprietarios();
                var telefones = this.telefoneRepository!.GetTelefones();

                var veiculoDTO = (
                    from veiculo in veiculos
                    join proprietario in proprietarios
                    on veiculo.ProprietarioCpfCnpj equals proprietario.CpfCnpj

                    select new VeiculoDTO
                    {
                        NumeroChassi = veiculo.NumeroChassi,
                        Modelo = veiculo.Modelo,
                        Ano = veiculo.Ano,
                        Cor = veiculo.Cor,
                        Valor = veiculo.Valor,
                        Quilometragem = veiculo.Quilometragem,
                        VersaoSistema = veiculo.VersaoSistema,
                        Proprietario = new ProprietarioDTO
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
                        }
                    }
                );

                return veiculoDTO;
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Veiculo).Name, e);

                return new HashSet<VeiculoDTO>();
            }
        }

        [HttpGet("{numeroChassi}")]
        public IActionResult GetVeiculoPorNumeroChassi(string numeroChassi)
        {
            try
            {
                var veiculo = this.veiculoRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

                if (veiculo is null)
                {
                    throw new NotFoundException(typeof(Veiculo).Name, $"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
                }

                var proprietarios = this.proprietarioRepository!.GetProprietarios();
                var acessorios = this.acessorioRepository!.GetAcessorios();
                var proprietario = proprietarios.Where(p => p.CpfCnpj == veiculo.ProprietarioCpfCnpj).ToList()[0];

                var resultado = new Veiculo
                {
                    NumeroChassi = veiculo.NumeroChassi,
                    Modelo = veiculo.Modelo,
                    Ano = veiculo.Ano,
                    Cor = veiculo.Cor,
                    Valor = veiculo.Valor,
                    Quilometragem = veiculo.Quilometragem,
                    VersaoSistema = veiculo.VersaoSistema,
                    ProprietarioCpfCnpj = proprietario.CpfCnpj,
                    Acessorios = acessorios.Where(a => a.VeiculoNumeroChassi == veiculo.NumeroChassi).ToList()
                };

                return Ok(resultado);
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Veiculo).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Veiculo).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpGet("busca")]
        public IActionResult GetPorQuilometragem(
            [FromQuery(Name = "quilometragem")] string? quilometragem = null,
            [FromQuery(Name = "versaoSistema")] string? versaoSistema = null)
        {
            try
            {
                var quilometragemVazia = string.IsNullOrEmpty(quilometragem);
                var versaoSistemaVazio = string.IsNullOrEmpty(versaoSistema);

                if (quilometragemVazia && versaoSistemaVazio)
                {
                    throw new BadRequestException(typeof(Veiculo).Name, "Parâmetros inválidos");
                }

                var veiculos = this.veiculoRepository!.GetVeiculos();

                if (!versaoSistemaVazio)
                {
                    veiculos = veiculos.Where(v => v.VersaoSistema == versaoSistema);
                }

                if (!quilometragemVazia)
                {
                    quilometragem = (quilometragem!.ToLower() == "desc" ? "desc" : "asc");
                }

                if (veiculos.Count() > 0)
                {
                    if (quilometragem == "desc")
                    {
                        veiculos = veiculos.OrderByDescending(v => v.Quilometragem);
                    }
                    else
                    {
                        veiculos = veiculos.OrderBy(v => v.Quilometragem);
                    }

                    return Ok(veiculos);
                }

                throw new NotFoundException(typeof(Veiculo).Name, "Nenhum veículo foi encontrado");
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(Veiculo).Name, e);

                return BadRequest(e.Message);
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Veiculo).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Veiculo).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpPost]
        public IActionResult PostVeiculo([FromBody] VeiculoForm form)
        {
            try
            {
                if (form is null)
                {
                    throw new BadRequestException(typeof(VeiculoForm).Name, "O formulário está inválido");
                }

                var veiculo = this.veiculoRepository!.InsertVeiculo(form.ToVeiculo());

                return Ok(veiculo);
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(VeiculoForm).Name, e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(VeiculoForm).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpPut("{numeroChassi}")]
        public IActionResult UpdateVeiculo(string numeroChassi, [FromBody] VeiculoForm form)
        {
            try
            {
                var veiculo = this.veiculoRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

                if (veiculo is null)
                {
                    throw new NotFoundException(typeof(Veiculo).Name, $"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
                }

                if (form is null)
                {
                    throw new BadRequestException(typeof(VeiculoForm).Name, "O formulário está inválido");
                }

                this.veiculoRepository.UpdateVeiculo(veiculo, form);

                veiculo = this.veiculoRepository.GetVeiculoPorNumeroChassi(numeroChassi);

                return Ok(veiculo);
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Veiculo).Name, e);

                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(VeiculoForm).Name, e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Veiculo).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpDelete("{numeroChassi}")]
        public IActionResult DeleteVeiculo(string numeroChassi)
        {
            try
            {
                var veiculo = this.veiculoRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

                if (veiculo is null)
                {
                    throw new NotFoundException(typeof(Veiculo).Name, $"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
                }

                this.veiculoRepository.DeleteVeiculo(numeroChassi);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Veiculo).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Veiculo).Name, e);

                return BadRequest("Erro não especificado");
            }
        }
    }
}
