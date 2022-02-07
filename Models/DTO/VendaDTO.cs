using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.Controllers.DTO
{
    public class VendaDTO
    {
        [Key]
        public int VendaId { get; set; }

        [Required]
        public DateTime DataVenda { get; set; }

        [Required]
        public double ValorVenda { get; set; }

        public Veiculo? Veiculo { get; set; }

        public Vendedor? Vendedor { get; set; }

        public VendaDTO() { }
    }
}
