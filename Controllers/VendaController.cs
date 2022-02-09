using CustomExceptions;
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
            var concessionariaContext = new ConcessionariaContext();

            this.vendaRepository = new VendaRepository(concessionariaContext);
            this.veiculoRepository = new VeiculoRepository(concessionariaContext);
            this.vendedorRepository = new VendedorRepository(concessionariaContext);
        }

        [HttpGet]
        public IEnumerable<VendaDTO> GetVendas()
        {
            try
            {
                var vendas = this.vendaRepository!.GetVendas();
                var veiculos = this.veiculoRepository!.GetVeiculos();
                var vendedores = this.vendedorRepository!.GetVendedores();

                var vendaDTO = (
                    from venda in vendas
                    join veiculo in veiculos
                    on venda.VeiculoNumeroChassi equals veiculo.NumeroChassi
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
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Venda).Name, e);

                return new HashSet<VendaDTO>();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetVendaPorID(int id)
        {
            try
            {
                var venda = this.vendaRepository!.GetVendaPorID(id);

                if (venda is null)
                {
                    throw new NotFoundException(typeof(Venda).Name, $"Venda com o id \"{id}\" não foi encontrada");
                }

                var veiculos = this.veiculoRepository!.GetVeiculos();
                var vendedores = this.vendedorRepository!.GetVendedores();

                venda.Veiculo = veiculos.Where(v => v.NumeroChassi == venda.VeiculoNumeroChassi).ToList()[0];
                venda.Vendedor = vendedores.Where(v => v.VendedorId == venda.VendedorId).ToList()[0];

                return Ok(venda);
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Venda).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Venda).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpPost]
        public IActionResult PostVenda([FromBody] VendaForm form)
        {
            try
            {
                if (form is null)
                {
                    throw new BadRequestException(typeof(VendaForm).Name, "O formulário está inválido");
                }

                this.vendaRepository!.InsertVenda(form.ToVenda());

                var venda = this.vendaRepository.GetUltimaVenda();

                return Ok(venda);
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(VendaForm).Name, e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(VendaForm).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVendedor(int id, [FromBody] VendaForm form)
        {
            try
            {
                var venda = this.vendaRepository!.GetVendaPorID(id);

                if (venda is null)
                {
                    throw new NotFoundException(typeof(Venda).Name, $"Venda com o id \"{id}\" não foi encontrada");
                }

                if (form is null)
                {
                    throw new BadRequestException(typeof(VendaForm).Name, "O formulário está inválido");
                }

                this.vendaRepository.UpdateVenda(venda, form);

                venda = this.vendaRepository.GetVendaPorID(id);

                return Ok(venda);
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Venda).Name, e);

                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                LogException.CriarLog(typeof(VendaForm).Name, e);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Venda).Name, e);

                return BadRequest("Erro não especificado");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVenda(int id)
        {
            try
            {
                var venda = this.vendaRepository!.GetVendaPorID(id);

                if (venda is null)
                {
                    throw new NotFoundException(typeof(Venda).Name, $"Venda com o id \"{id}\" não foi encontrada");
                }

                this.vendaRepository.DeleteVenda(id);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                LogException.CriarLog(typeof(Venda).Name, e);

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                LogException.CriarLog(typeof(Venda).Name, e);

                return BadRequest("Erro não especificado");
            }
        }
    }
}
