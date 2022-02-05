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

        public VeiculoController()
        {
            this.veiculoRepository = new VeiculoRepository(new ConcessionariaContext());
            this.proprietarioRepository = new ProprietarioRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<VeiculoDTO> GetVeiculos()
        {
            var veiculos = this.veiculoRepository!.GetVeiculos();
            var proprietarios = this.proprietarioRepository!.GetProprietarios();

            var veiculoDTO = veiculos.Join(
                proprietarios,
                veiculo => veiculo.ProprietarioCpfCnpj,
                proprietario => proprietario.CpfCnpj,

                (v, p) => new VeiculoDTO
                {
                    NumeroChassi = v.NumeroChassi,
                    Proprietario = p,
                    Modelo = v.Modelo,
                    Ano = v.Ano,
                    Valor = v.Valor,
                    Quilometragem = v.Quilometragem,
                    Cor = v.Cor,
                    VersaoSistema = v.VersaoSistema
                }
            );

            return veiculoDTO;
        }

        [HttpGet("{numeroChassi}")]
        public IActionResult GetVeiculoPorNumeroChassi(string numeroChassi)
        {
            var veiculo = this.veiculoRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

            if (veiculo is null)
            {
                return NotFound($"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
            }

            var proprietarios = this.proprietarioRepository!.GetProprietarios();

            var veiculoDTO = new VeiculoDTO
            {
                NumeroChassi = veiculo.NumeroChassi,
                Proprietario = proprietarios.Where(p => p.CpfCnpj == veiculo.ProprietarioCpfCnpj).ToList()[0],
                Modelo = veiculo.Modelo,
                Ano = veiculo.Ano,
                Valor = veiculo.Valor,
                Quilometragem = veiculo.Quilometragem,
                Cor = veiculo.Cor,
                VersaoSistema = veiculo.VersaoSistema
            };

            return Ok(veiculoDTO);
        }

        [HttpPost]
        public IActionResult PostVeiculo([FromBody] VeiculoForm form)
        {
            if (form is null)
            {
                return BadRequest();
            }

            this.veiculoRepository!.InsertVeiculo(form.ToVeiculo());

            // Não necessariamente irá pegar o veículo criado 
            // (já que ordena pelo chassi, que é uma string)
            var veiculo = this.veiculoRepository.GetUltimoVeiculo();

            return Ok(veiculo);
        }

        [HttpPut("{numeroChassi}")]
        public IActionResult UpdateVeiculo(string numeroChassi, [FromBody] VeiculoForm form)
        {
            if (form is null)
            {
                return BadRequest();
            }

            var veiculo = this.veiculoRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

            if (veiculo is null)
            {
                return NotFound($"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
            }

            this.veiculoRepository.UpdateVeiculo(veiculo, form);

            veiculo = this.veiculoRepository.GetVeiculoPorNumeroChassi(numeroChassi);

            return Ok(veiculo);
        }

        [HttpDelete("{numeroChassi}")]
        public IActionResult DeleteVeiculo(string numeroChassi)
        {
            var veiculo = this.veiculoRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

            if (veiculo is null)
            {
                return NotFound($"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
            }

            this.veiculoRepository.DeleteVeiculo(numeroChassi);

            return Accepted();
        }
    }
}
