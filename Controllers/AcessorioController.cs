using CustomExceptions;
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
            try
            {
                return this.acessorioRepository!.GetAcessorios();
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Acessorio).Name, e);

                return new HashSet<Acessorio>();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAcessorioPorID(int id)
        {
            try
            {
                var acessorio = this.acessorioRepository!.GetAcessorioPorID(id);

                if (acessorio is null)
                {
                    throw new NotFoundException(typeof(Acessorio).Name, $"Acessório com o id \"{id}\" não foi encontrado");
                }

                return Ok(acessorio);
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Acessorio).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Acessorio).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpPost]
        public IActionResult PostAcessorio([FromBody] AcessorioForm form)
        {
            try
            {
                if (form is null)
                {
                    throw new BadRequestException(typeof(AcessorioForm).Name, "O formulário está inválido");
                }

                this.acessorioRepository!.InsertAcessorio(form.ToAcessorio());

                var acessorio = this.acessorioRepository.GetUltimoAcessorio();

                return Ok(acessorio);
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(AcessorioForm).Name, e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(AcessorioForm).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAcessorio(int id, [FromBody] AcessorioForm form)
        {
            try
            {
                var acessorio = this.acessorioRepository!.GetAcessorioPorID(id);

                if (acessorio is null)
                {
                    throw new NotFoundException(typeof(Acessorio).Name, $"Acessório com o id \"{id}\" não foi encontrado");
                }

                if (form is null)
                {
                    throw new BadRequestException(typeof(AcessorioForm).Name, "O formulário está inválido");
                }

                this.acessorioRepository.UpdateAcessorio(acessorio, form);

                acessorio = this.acessorioRepository.GetAcessorioPorID(id);

                return Ok(acessorio);
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Acessorio).Name, e);

                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(AcessorioForm).Name, e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Acessorio).Name, e);

                return BadRequest("Erro não especificado");
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
                    throw new NotFoundException(typeof(Acessorio).Name, $"Acessório com o id \"{id}\" não foi encontrado");
                }

                this.acessorioRepository.DeleteAcessorio(id);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Acessorio).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Acessorio).Name, e);

                return BadRequest("Erro não especificado");
            }
        }
    }
}
