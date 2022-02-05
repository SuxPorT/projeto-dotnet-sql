using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private IVeiculoRepository? pessoaRepository;

        public VeiculoController()
        {
            this.pessoaRepository = new VeiculoRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<Veiculo> GetVeiculos()
        {
            return this.pessoaRepository!.GetVeiculos();
        }

        [HttpGet("{numeroChassi}")]
        public IActionResult GetVendedorPorNumeroChassi(string numeroChassi)
        {
            var veiculo = this.pessoaRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

            if (veiculo is null)
            {
                return NotFound($"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
            }

            return Ok(veiculo);
        }

        [HttpPost]
        public IActionResult PostVeiculo([FromBody] VeiculoForm veiculoForm)
        {
            if (veiculoForm is null)
            {
                return BadRequest();
            }

            this.pessoaRepository!.InsertVeiculo(veiculoForm.ToVeiculo());

            // Não necessariamente irá pegar o veículo criado 
            // (já que ordena pelo chassi, que é uma string)
            var veiculo = this.pessoaRepository.GetUltimoVeiculo();

            return Ok(veiculo);
        }

        [HttpPut("{numeroChassi}")]
        public IActionResult UpdateVendedor(string numeroChassi, [FromBody] VeiculoForm veiculoForm)
        {
            if (veiculoForm is null)
            {
                return BadRequest();
            }

            var entity = this.pessoaRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

            if (entity is null)
            {
                return NotFound($"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
            }

            this.pessoaRepository.UpdateVeiculo(entity, veiculoForm);

            entity = this.pessoaRepository.GetVeiculoPorNumeroChassi(numeroChassi);

            return Ok(entity);
        }

        [HttpDelete("{numeroChassi}")]
        public IActionResult DeleteVendedor(string numeroChassi)
        {
            var vendedor = this.pessoaRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

            if (vendedor is null)
            {
                return NotFound($"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
            }

            this.pessoaRepository.DeleteVeiculo(numeroChassi);

            return Ok();
        }
    }
}
