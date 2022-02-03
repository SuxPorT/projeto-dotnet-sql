using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.Models;

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
    }
}