using Microsoft.AspNetCore.Mvc;
using projeto_dotnet_sql.DAL;
using projeto_dotnet_sql.DAL.Interfaces;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcessorioController : ControllerBase
    {
        private IAcessorioRepository? acessorioRepository;

        public AcessorioController()
        {
            this.acessorioRepository = new AcessorioRepository(new ConcessionariaContext());
        }

        [HttpGet]
        public IEnumerable<Acessorio> GetAcessorios()
        {
            return this.acessorioRepository!.GetAcessorios();
        }

        [HttpGet("{id}")]
        public IActionResult GetAcessorioPorID(int id)
        {
            try
            {
                var acessorio = this.acessorioRepository!.GetAcessorioPorID(id);

                if (acessorio is null)
                {
                    return NotFound($"Acessório com o id \"{id}\" não foi encontrado");
                }

                return Ok(acessorio);
             }
             catch (NotFoundException e)
             {
                e.CriarLog();
                
                return NotFound(e.Message);
             }
                
        }

        [HttpPost]
        public IActionResult PostAcessorio([FromBody] AcessorioForm form)
        {
            try
            {
                if (form is null)
                {
                    return BadRequest();
                }

                this.acessorioRepository!.InsertAcessorio(form.ToAcessorio());

                var acessorio = this.acessorioRepository.GetUltimoAcessorio();

                return Ok(acessorio);
            }
            catch (BadRequestException e)
            {
                e.CriarLog();
                
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAcessorio(int id, [FromBody] AcessorioForm form)
        {
            try
            {
                if (form is null)
                {
                    return BadRequest();
                }

                var acessorio = this.acessorioRepository!.GetAcessorioPorID(id);

                if (acessorio is null)
                {
                    return NotFound($"Acessório com o id \"{id}\" não foi encontrado");
                }

                this.acessorioRepository.UpdateAcessorio(acessorio, form);

                acessorio = this.acessorioRepository.GetAcessorioPorID(id);

                return Ok(acessorio);
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
        public IActionResult DeleteAcessorio(int id)
        {
            try
            {
                var acessorio = this.acessorioRepository!.GetAcessorioPorID(id);

                if (acessorio is null)
                {
                    return NotFound($"Acessório com o id \"{id}\" não foi encontrado");
                }

                this.acessorioRepository.DeleteAcessorio(id);

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
