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
        [ForeignKey("NumeroChassi")]
        public string? NumeroChassi { get; set; }

        [Required]
        [ForeignKey("VendedorId")]
        public int VendedorId { get; set; }

        public Venda() { }
    }
}
