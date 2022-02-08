using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models
{
    public class Veiculo
    {
        [Key]
        [MaxLength(17, ErrorMessage = "O número do chassi deve possuir no máximo 17 caracteres")]
        public string? NumeroChassi { get; set; }

        [Required(ErrorMessage = "O campo \"modelo\" é necessário")]
        [MaxLength(50, ErrorMessage = "O modelo deve possuir no máximo 50 caracteres")]
        public string? Modelo { get; set; }

        [Required(ErrorMessage = "O campo \"ano\" é necessário")]
        public int Ano { get; set; }

        [MaxLength(30, ErrorMessage = "A cor deve possuir no máximo 30 caracteres")]
        public string? Cor { get; set; } = null;

        [Required(ErrorMessage = "O campo \"valor\" é necessário")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "O campo \"quilometragem\" é necessário")]
        public double Quilometragem { get; set; }

        [MaxLength(10, ErrorMessage = "A versão do sistema deve possuir no máximo 10 caracteres")]
        public string? VersaoSistema { get; set; } = null;

        [ForeignKey("ProprietarioCpfCnpj")]
        [Required(ErrorMessage = "O campo \"proprietarioCpfCnpj\" é necessário")]
        public string? ProprietarioCpfCnpj { get; set; }

        public ICollection<Acessorio> Acessorios { get; set; }

        public Veiculo()
        {
            Acessorios = new HashSet<Acessorio>();
        }
    }
}
