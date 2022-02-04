using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private IVeiculoRepository? veiculoRepository;

        public VeiculosController()
        {
            this.veiculoRepository = new VeiculoRepository(new ConcessionariaContext());
        }



        [HttpGet]
        public IEnumerable<Veiculos> GetVeiculos()
        {
            return this.veiculoRepository!.GetVeiculos();
        }

         [HttpPost]
        public Veiculos? Post([FromBody] Veiculos veiculo)
        {
            using (var _context = new ConcessionariaContext())
            {
                _context.Veiculos.Add(veiculo);
                _context.SaveChanges();
                return veiculo;//_context.Students.Find(student.StudentId);
            }
        }

         [HttpPut("{id}")]
        public void Put(string id,[FromBody] Veiculos veiculo)
        {
            using(var _context = new ConcessionariaContext())
            {
                var entity = _context.Veiculos.Find(id);
                if(entity == null)
                {
                    return ;
                }
                _context.Entry(entity).CurrentValues.SetValues(veiculo);
                _context.SaveChanges();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            using(var _context = new ConcessionariaContext())
            {
                var veiculo = _context.Veiculos
                                .FirstOrDefault(s => s.NumeroChassi == id);
                if(veiculo == null)
                {
                    return NotFound();
                }
                return Ok(veiculo);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            using(var _context = new ConcessionariaContext())
            {
                var entity = _context.Veiculos.Find(id);
                if(entity == null)
                {
                    return ;
                }
                _context.Veiculos.Remove(entity);
                _context.SaveChanges();
            }
        }


    }
}