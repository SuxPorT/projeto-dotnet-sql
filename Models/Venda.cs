using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models
{
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

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

        public Venda() { }
    }
}
