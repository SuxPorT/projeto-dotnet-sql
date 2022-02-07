using System.ComponentModel.DataAnnotations;
using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.Controllers.DTO
{
    public class ProprietarioForm
    {
        [Key]
        public string? CpfCnpj { get; set; }

        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string? Nome { get; set; }

        [MaxLength(50, ErrorMessage = "O email deve ter no máximo 50 caracteres")]
        public string? Email { get; set; }

        public DateTime DataNascimento { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? CEP { get; set; }

        public ProprietarioForm() { }

        public Proprietario ToProprietario()
        {
            return new Proprietario
            {
                CpfCnpj = this.CpfCnpj,
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
