using System.ComponentModel.DataAnnotations;

namespace projeto_dotnet_sql.Models
{
    public class Acessorio
    {
        [Key]
        public int AcessorioId { get; set; }

        [Required(ErrorMessage = "O campo \"descricao\" é necessário")]
        [MaxLength(50, ErrorMessage = "A descrição deve possuir no máximo 50 caracteres")]
        public string? Descricao { get; set; }

        public Acessorio() { }
    }
}
