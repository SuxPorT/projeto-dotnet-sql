using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private IVendaRepository? vendaRepository;

        public VendaController()
        {
            this.vendaRepository = new VendaRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<Venda> GetVendas()
        {
            return this.vendaRepository!.GetVendas();
        }

        [HttpGet("{id}")]
        public IActionResult GetVendaPorID(int id)
        {
            var venda = this.vendaRepository!.GetVendaPorID(id);

            if (venda is null)
            {
                return NotFound($"Venda com o id \"{id}\" não foi encontrada");
            }

            return Ok(venda);
        }
    }
}
