using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models
{
    public class Telefone
    {
        [Key]
        public int TelefoneId { get; set; }

        [ForeignKey("ProprietarioCpfCnpj")]
        [Required(ErrorMessage = "O campo \"proprietarioCpfCnpj\" é necessário")]
        public string? ProprietarioCpfCnpj { get; set; }

        [Required(ErrorMessage = "O campo \"codigo\" é necessário")]
        [MinLength(9, ErrorMessage = "O telefone deve possuir no mínimo 9 caracteres")]
        [MaxLength(20, ErrorMessage = "O telefone deve possuir no máximo 20 caracteres")]
        public string? Codigo { get; set; }

        public Telefone() { }
    }
}
