using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models
{
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

        [Required(ErrorMessage = "O campo \"dataVenda\" é necessário")]
        public DateTime DataVenda { get; set; }

        [Required(ErrorMessage = "O campo \"valorVenda\" é necessário")]
        public double ValorVenda { get; set; }

        [ForeignKey("VeiculoNumeroChassi")]
        [Required(ErrorMessage = "O campo \"veiculoNumeroChassi\" é necessário")]
        [MaxLength(17, ErrorMessage = "O número do chassi do veículo deve possuir no máximo 17 caracteres")]
        public string? VeiculoNumeroChassi { get; set; }
        public Veiculo? Veiculo { get; set; }

        [ForeignKey("VendedorId")]
        [Required(ErrorMessage = "O campo \"vendedorId\" é necessário")]
        public int VendedorId { get; set; }
        public Vendedor? Vendedor { get; set; }

        public Venda() { }
    }
}
