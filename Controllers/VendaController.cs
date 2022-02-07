using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.Controllers.DTO;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.DAL.Interfaces;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private IVendaRepository? vendaRepository;
        private IVeiculoRepository? veiculoRepository;
        private IVendedorRepository? vendedorRepository;

        public VendaController()
        {
            this.vendaRepository = new VendaRepository(new ConcessionariaContext());
            this.veiculoRepository = new VeiculoRepository(new ConcessionariaContext());
            this.vendedorRepository = new VendedorRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<VendaDTO> GetVendas()
        {
            var vendas = this.vendaRepository!.GetVendas();
            var veiculos = this.veiculoRepository!.GetVeiculos();
            var vendedores = this.vendedorRepository!.GetVendedores();

            var vendaDTO = (
                from venda in vendas
                join veiculo in veiculos
                on venda.NumeroChassi equals veiculo.NumeroChassi
                join vendedor in vendedores
                on venda.VendedorId equals vendedor.VendedorId

                select new VendaDTO
                {
                    VendaId = venda.VendaId,
                    DataVenda = venda.DataVenda,
                    ValorVenda = veiculo.Valor,
                    Veiculo = veiculo,
                    Vendedor = vendedor
                }
            );

            return vendaDTO;
        }

        [HttpGet("{id}")]
        public IActionResult GetVendaPorID(int id)
        {
            var venda = this.vendaRepository!.GetVendaPorID(id);

            if (venda is null)
            {
                return NotFound($"Venda com o id \"{id}\" não foi encontrada");
            }

            var veiculos = this.veiculoRepository!.GetVeiculos();
            var vendedores = this.vendedorRepository!.GetVendedores();

            venda.Veiculo = veiculos.Where(v => v.NumeroChassi == venda.NumeroChassi).ToList()[0];
            venda.Vendedor = vendedores.Where(v => v.VendedorId == venda.VendedorId).ToList()[0];

            return Ok(venda);
        }

        [HttpPost]
        public IActionResult PostVenda([FromBody] VendaForm form)
        {
            if (form is null)
            {
                return BadRequest();
            }

            this.vendaRepository!.InsertVenda(form.ToVenda());

            var venda = this.vendaRepository.GetUltimaVenda();

            return Ok(venda);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVendedor(int id, [FromBody] VendaForm form)
        {
            if (form is null)
            {
                return BadRequest();
            }

            var venda = this.vendaRepository!.GetVendaPorID(id);

            if (venda is null)
            {
                return NotFound($"Venda com o id \"{id}\" não foi encontrado");
            }

            this.vendaRepository.UpdateVenda(venda, form);

            venda = this.vendaRepository.GetVendaPorID(id);

            return Ok(venda);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVenda(int id)
        {
            var venda = this.vendaRepository!.GetVendaPorID(id);

            if (venda is null)
            {
                return NotFound($"Venda com o id \"{id}\" não foi encontrado");
            }

            this.vendaRepository.DeleteVenda(id);

            return Accepted();
        }
    }
}
