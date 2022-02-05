using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models.Form
{
    public class VendaForm
    {
        [Required]
        public DateTime DataVenda { get; set; }

        [Required]
        public double ValorComissao { get; set; }

        [Required]
        [ForeignKey("Veiculo")]
        public string? NumeroChassi { get; set; }
        public Veiculo? Veiculo { get; set; }

        [Required]
        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }
        public Vendedor? Vendedor { get; set; }

        public VendaForm() { }

        public Venda ToVenda()
        {
            return new Venda
            {
                DataVenda = this.DataVenda,
                ValorComissao = this.ValorComissao,
                NumeroChassi = this.NumeroChassi,
                Veiculo = this.Veiculo,
                VendedorId = this.VendedorId,
                Vendedor = this.Vendedor
            };
        }
    }
}
