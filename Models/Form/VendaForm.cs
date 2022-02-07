using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models.Form
{
    public class VendaForm
    {
        [Required]
        public DateTime DataVenda { get; set; }

        [Required]
        public double ValorVenda { get; set; }

        [Required]
        [ForeignKey("Veiculo")]
        public string? NumeroChassi { get; set; }

        [Required]
        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }

        public VendaForm() { }

        public Venda ToVenda()
        {
            return new Venda
            {
                DataVenda = this.DataVenda,
                ValorVenda = this.ValorVenda,
                NumeroChassi = this.NumeroChassi,
                VendedorId = this.VendedorId,
            };
        }
    }
}
