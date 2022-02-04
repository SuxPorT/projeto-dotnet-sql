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
        private IVeiculoRepository? veiculoRepository;

        public VeiculoController()
        {
            this.veiculoRepository = new VeiculoRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<Veiculo> GetVeiculos()
        {
            return this.veiculoRepository!.GetVeiculos();
        }

        [HttpGet("{numeroChassi}")]
        public IActionResult GetVendedorPorNumeroChassi(string numeroChassi)
        {
            var veiculo = this.veiculoRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

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

            this.veiculoRepository!.InsertVeiculo(veiculoForm.ToVeiculo());

            // Não necessariamente irá pegar o veículo criado 
            // (já que ordena pelo chassi)
            var veiculo = this.veiculoRepository.GetUltimoVeiculo();

            return Ok(veiculo);
        }

        [HttpPut("{numeroChassi}")]
        public IActionResult UpdateVendedor(string numeroChassi, [FromBody] VeiculoForm veiculoForm)
        {
            if (veiculoForm is null)
            {
                return BadRequest();
            }

            var entity = this.veiculoRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

            if (entity is null)
            {
                return NotFound($"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
            }

            this.veiculoRepository.UpdateVeiculo(entity, veiculoForm);

            entity = this.veiculoRepository.GetVeiculoPorNumeroChassi(numeroChassi);

            return Ok(entity);
        }

        [HttpDelete("{numeroChassi}")]
        public IActionResult DeleteVendedor(string numeroChassi)
        {
            var vendedor = this.veiculoRepository!.GetVeiculoPorNumeroChassi(numeroChassi);

            if (vendedor is null)
            {
                return NotFound($"Veículo com o chassi \"{numeroChassi}\" não foi encontrado");
            }

            this.veiculoRepository.DeleteVeiculo(numeroChassi);

            return Ok();
        }
    }
}
