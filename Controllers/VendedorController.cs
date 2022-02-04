using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendedorController : ControllerBase
    {
        private IVendedorRepository? vendedorRepository;

        public VendedorController()
        {
            this.vendedorRepository = new VendedorRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<Vendedor> GetVendedores()
        {
            return this.vendedorRepository!.GetVendedores();
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
        public IActionResult PostVendedor([FromBody] VendedorForm vendedorForm)
        {
            if (vendedorForm is null)
            {
                return BadRequest();
            }

            this.vendedorRepository!.InsertVendedor(vendedorForm.ToVendedor());

            var vendedor = this.vendedorRepository.GetUltimoVendedor();

            return Ok(vendedor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVendedor(int id, [FromBody] VendedorForm vendedorForm)
        {
            if (vendedorForm is null)
            {
                return BadRequest();
            }

            var entity = this.vendedorRepository!.GetVendedorPorID(id);

            if (entity is null)
            {
                return NotFound($"Vendedor com o id \"{id}\" não foi encontrado");
            }

            this.vendedorRepository.UpdateVendedor(entity, vendedorForm);

            entity = this.vendedorRepository.GetVendedorPorID(id);

            return Ok(entity);
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

            return Ok();
        }
    }
}
