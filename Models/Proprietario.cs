using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models
{
    public class Proprietario
    {
        [Key]
        public string? CpfCnpj { get; set; }

        [MaxLength(1, ErrorMessage = "O campo deve indicar \"F\" ou \"J")]
        public char IndicadorPessoa { get; set; }

        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string? Nome { get; set; }

        [MaxLength(50, ErrorMessage = "O email deve ter no máximo 50 caracteres")]
        public string? Email { get; set; }

        [ForeignKey("TelefoneId")]
        public int TelefoneId { get; set; }

        public DateTime DataNascimento { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CEP { get; set; }

        public Proprietario() { }
    }
}
