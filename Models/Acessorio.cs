using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models
{
    public class Acessorio
    {
        [Key]
        public int AcessorioId { get; set; }

        [Required(ErrorMessage = "O campo \"veiculoNumeroChassi\" é necessário")]
        [ForeignKey("VeiculoNumeroChassi")]
        [MaxLength(17, ErrorMessage = "O numero do chassi deve possuir no máximo 17 caracteres")]
        public string? VeiculoNumeroChassi { get; set; }

        [Required(ErrorMessage = "O campo \"descricao\" é necessário")]
        [MaxLength(50, ErrorMessage = "A descricao deve possuir no máximo 50 caracteres")]
        public string? Descricao { get; set; }

        public Acessorio() { }
    }
}
