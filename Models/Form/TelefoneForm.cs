using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models.Form
{
    public class TelefoneForm
    {
        [ForeignKey("ProprietarioCpfCnpj")]
        [Required(ErrorMessage = "O campo \"proprietarioCpfCnpj\" é necessário")]
        public string? ProprietarioCpfCnpj { get; set; }

        [Required(ErrorMessage = "O campo \"codigo\" é necessário")]
        [MinLength(9, ErrorMessage = "O telefone deve possuir no mínimo 9 caracteres")]
        [MaxLength(20, ErrorMessage = "O telefone deve possuir no máximo 20 caracteres")]
        public string? Codigo { get; set; }

        public TelefoneForm() { }

        public Telefone ToTelefone()
        {
            return new Telefone
            {
                ProprietarioCpfCnpj = this.ProprietarioCpfCnpj,
                Codigo = this.Codigo
            };
        }
    }
}
