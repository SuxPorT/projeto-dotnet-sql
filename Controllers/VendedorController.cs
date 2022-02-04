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

         [HttpPost]
        public Vendedor? Post([FromBody] Vendedor vendedor)
        {
            using (var _context = new ConcessionariaContext())
            {
                _context.Vendedores.Add(vendedor);
                _context.SaveChanges();
                return vendedor;//_context.Students.Find(student.StudentId);
            }
        }

         [HttpPut("{id}")]
        public void Put(int id,[FromBody] Vendedor vendedor)
        {
            using(var _context = new ConcessionariaContext())
            {
                var entity = _context.Vendedores.Find(id);
                if(entity == null)
                {
                    return ;
                }
                _context.Entry(entity).CurrentValues.SetValues(vendedor);
                _context.SaveChanges();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using(var _context = new ConcessionariaContext())
            {
                var vendedor = _context.Vendedores
                                .FirstOrDefault(s => s.VendedorId == id);
                if(vendedor == null)
                {
                    return NotFound();
                }
                return Ok(vendedor);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using(var _context = new ConcessionariaContext())
            {
                var entity = _context.Vendedores.Find(id);
                if(entity == null)
                {
                    return ;
                }
                _context.Vendedores.Remove(entity);
                _context.SaveChanges();
            }
        }


    }
}