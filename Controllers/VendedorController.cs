using Exceptions;
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

            var vendedorDTO = (
                from vendedor in vendedores
                join venda in vendas
                on vendedor.VendedorId equals venda.VendedorId

                select new VendedorDTO
                {
                    VendedorId = vendedor.VendedorId,
                    Nome = vendedor.Nome,
                    SalarioBase = vendedor.SalarioBase + venda.ValorVenda * 0.01,
                    Vendas = vendas.Where(v => v.VendedorId == vendedor.VendedorId).ToList()
                }
            );

            return vendedorDTO;
        }

        [HttpGet("{id}")]
        public IActionResult GetVendedorPorID(int id)
        {
            try
            {
                var vendedor = this.vendedorRepository!.GetVendedorPorID(id);

                if (vendedor is null)
                {
                    throw new NotFoundException(typeof(Vendedor).Name, $"Vendedor com o id \"{id}\" não foi encontrado");
                }

                return Ok(vendedor);
            }
            catch (NotFoundException e)
            {
                e.CriarLog();

                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult PostVendedor([FromBody] VendedorForm form)
        {
            try
            {
                if (form is null)
                {
                    throw new BadRequestException(typeof(VendedorForm).Name, "O formulário está vazio");
                }

                this.vendedorRepository!.InsertVendedor(form.ToVendedor());

                var vendedor = this.vendedorRepository.GetUltimoVendedor();

                return Ok(vendedor);
            }
            catch (BadRequestException e)
            {
                e.CriarLog();

                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateVendedor(int id, [FromBody] VendedorForm form)
        {
            try
            {
                if (form is null)
                {
                    throw new BadRequestException(typeof(VendedorForm).Name, "O formulário está vazio");
                }

                var vendedor = this.vendedorRepository!.GetVendedorPorID(id);

                if (vendedor is null)
                {
                    throw new NotFoundException(typeof(Vendedor).Name, $"Vendedor com o id \"{id}\" não foi encontrado");
                }

                this.vendedorRepository.UpdateVendedor(vendedor, form);

                vendedor = this.vendedorRepository.GetVendedorPorID(id);

                return Ok(vendedor);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteVendedor(int id)
        {
            try
            {
                var vendedor = this.vendedorRepository!.GetVendedorPorID(id);

                if (vendedor is null)
                {
                    throw new NotFoundException(typeof(Vendedor).Name, $"Vendedor com o id \"{id}\" não foi encontrado");
                }

                this.vendedorRepository.DeleteVendedor(id);

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
