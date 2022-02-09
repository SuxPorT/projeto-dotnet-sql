using System.ComponentModel.DataAnnotations;

namespace projeto_dotnet_sql.Models.DTO
{
    public class ProprietarioDTO
    {
        [Key]
        public string? CpfCnpj { get; set; }

        [MaxLength(1, ErrorMessage = "O campo deve indicar \"F\" ou \"J")]
        public string? IndicadorPessoa { get; set; }

        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string? Nome { get; set; }

        [MaxLength(50, ErrorMessage = "O email deve ter no máximo 50 caracteres")]
        public string? Email { get; set; }

        public ICollection<Telefone> Telefones { get; set; }

        public DateTime DataNascimento { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? CEP { get; set; }

        public ProprietarioDTO()
        {
            this.Telefones = new HashSet<Telefone>();
        }
    }
}
