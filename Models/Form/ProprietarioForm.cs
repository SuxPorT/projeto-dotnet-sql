using System.ComponentModel.DataAnnotations;
using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.Controllers.DTO
{
    public class ProprietarioForm
    {
        [MaxLength(1, ErrorMessage = "O campo deve indicar \"F\" ou \"J")]
        public char IndicadorPessoa { get; set; }

        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string? Nome { get; set; }

        [MaxLength(50, ErrorMessage = "O email deve ter no máximo 50 caracteres")]
        public string? Email { get; set; }

        public List<Telefone> Telefones { get; set; } = new List<Telefone>();

        public DateTime DataNascimento { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? CEP { get; set; }

        public ProprietarioForm() { }

        public Proprietario ToProprietario()
        {
            return new Proprietario
            {
                IndicadorPessoa = this.IndicadorPessoa,
                Nome = this.Nome,
                Email = this.Email,
                Telefones = this.Telefones,
                DataNascimento = this.DataNascimento,
                Cidade = this.Cidade,
                UF = this.UF,
                CEP = this.CEP
            };
        }
    }
}
