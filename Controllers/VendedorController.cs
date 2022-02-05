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
    public class VendedorController : ControllerBase
    {
        private IVendedorRepository? vendedorRepository;
        private IVendaRepository? vendaRepository;

        public VendedorController()
        {
            this.vendedorRepository = new VendedorRepository(new ConcessionariaContext());
            this.vendaRepository = new VendaRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<VendedorDTO> GetVendedores()
        {
            var vendedores = this.vendedorRepository!.GetVendedores();
            var vendas = this.vendaRepository!.GetVendas();

            var vendedorDTO = vendedores.Join(
                vendas,
                vendedor => vendedor.VendedorId,
                venda => venda.VendedorId,

                (vendedor, venda) => new VendedorDTO
                {
                    VendedorId = vendedor.VendedorId,
                    Nome = vendedor.Nome,
                    SalarioMinimo = vendedor.SalarioMinimo,
                    Vendas = new List<Venda> { venda }
                }
            );

            return vendedorDTO;
        }

        [HttpGet("{id}")]
        public IActionResult GetVendedorPorID(int id)
        {
            var vendedor = this.vendedorRepository!.GetVendedorPorID(id);

            if (vendedor is null)
            {
                return NotFound($"Vendedor com o id \"{id}\" não foi encontrado");
            }

            return Ok(vendedor);
        }

        [HttpPost]
        public IActionResult PostVendedor([FromBody] VendedorForm form)
        {
            if (form is null)
            {
                return BadRequest();
            }

            this.vendedorRepository!.InsertVendedor(form.ToVendedor());

            var vendedor = this.vendedorRepository.GetUltimoVendedor();

            return Ok(vendedor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVendedor(int id, [FromBody] VendedorForm form)
        {
            if (form is null)
            {
                return BadRequest();
            }

            var vendedor = this.vendedorRepository!.GetVendedorPorID(id);

            if (vendedor is null)
            {
                return NotFound($"Vendedor com o id \"{id}\" não foi encontrado");
            }

            this.vendedorRepository.UpdateVendedor(vendedor, form);

            vendedor = this.vendedorRepository.GetVendedorPorID(id);

            return Ok(vendedor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVendedor(int id)
        {
            var vendedor = this.vendedorRepository!.GetVendedorPorID(id);

            if (vendedor is null)
            {
                return NotFound($"Vendedor com o id \"{id}\" não foi encontrado");
            }

            this.vendedorRepository.DeleteVendedor(id);

            return Accepted();
        }
    }
}
