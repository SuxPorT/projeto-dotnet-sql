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
                    Proprietario = proprietario
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
                Modelo = veiculo.Modelo,
                Ano = veiculo.Ano,
                Cor = veiculo.Cor,
                Valor = veiculo.Valor,
                Quilometragem = veiculo.Quilometragem,
                VersaoSistema = veiculo.VersaoSistema,
                Proprietario = proprietarios.Where(p => p.CpfCnpj == veiculo.ProprietarioCpfCnpj).ToList()[0]
            };

            return Ok(veiculoDTO);
        }

        [HttpGet("busca")]
        public IActionResult GetPorQuilometragem(
            [FromQuery(Name = "quilometragem")] string quilometragem,
            [FromQuery(Name = "versaoSistema")] string versaoSistema)
        {
            if ((quilometragem == null && versaoSistema == null) || (quilometragem == "" && versaoSistema == ""))
            {
                return BadRequest();
            }

            var veiculos = this.veiculoRepository!.GetVeiculos();
            var resultado = new HashSet<Veiculo>();

            foreach (var veiculo in veiculos)
            {
                if (veiculo.Quilometragem == Double.Parse(quilometragem!) && veiculo.VersaoSistema == versaoSistema)
                {
                    resultado.Add(veiculo);
                }
            }

            if (resultado.Count > 0)
            {
                return Ok(resultado);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult PostVeiculo([FromBody] VeiculoForm form)
        {
            if (form is null)
            {
                return BadRequest();
            }

            var veiculo = this.veiculoRepository!.InsertVeiculo(form.ToVeiculo());

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
