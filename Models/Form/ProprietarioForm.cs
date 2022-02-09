using System.ComponentModel.DataAnnotations;
using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.Controllers.DTO
{
    public class ProprietarioForm
    {
        [Key]
        [MaxLength(20, ErrorMessage = "O cpfCnpj deve ter no máximo 20 caracteres")]
        public string? CpfCnpj { get; set; }

        [Required(ErrorMessage = "O campo \"indicadorPessoa\" é necessário")]
        [MaxLength(1, ErrorMessage = "O campo deve indicar \"F\" ou \"J")]
        public string? IndicadorPessoa { get; set; }

        [Required(ErrorMessage = "O campo \"nome\" é necessário")]
        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo \"email\" é necessário")]
        [MaxLength(50, ErrorMessage = "O email deve ter no máximo 50 caracteres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo \"dataNascimento\" é necessário")]
        public DateTime DataNascimento { get; set; }

        [MaxLength(50, ErrorMessage = "A cidade deve ter no máximo 50 caracteres")]
        public string? Cidade { get; set; }

        [MaxLength(40, ErrorMessage = "A UF deve ter no máximo 40 caracteres")]
        public string? UF { get; set; }

        [MaxLength(10, ErrorMessage = "O CEP deve ter no máximo 10 caracteres")]
        public string? CEP { get; set; }

        public ProprietarioForm() { }

        public Proprietario ToProprietario()
        {
            return new Proprietario
            {
                CpfCnpj = this.CpfCnpj,
                IndicadorPessoa = this.IndicadorPessoa,
                Nome = this.Nome,
                Email = this.Email,
                DataNascimento = this.DataNascimento,
                Cidade = this.Cidade,
                UF = this.UF,
                CEP = this.CEP
            };
        }
    }
}
