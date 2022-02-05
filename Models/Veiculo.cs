using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models
{
    public class Veiculo
    {
        [Key]
        public string? NumeroChassi { get; set; }

        [ForeignKey("DocumentoProprietario")]
        public string? ProprietarioCpfCnpj { get; set; }

        [Required(ErrorMessage = "O campo \"Modelo\" é necessário")]
        [MaxLength(50)]
        public string? Modelo { get; set; }

        [Required(ErrorMessage = "O campo \"Ano\" é necessário")]
        [MaxLength(30)]
        public int Ano { get; set; }

        [Required(ErrorMessage = "O campo \"Valor\" é necessário")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "O campo \"Quilometragem\" é necessário")]
        public double Quilometragem { get; set; }

        [MaxLength(30)]
        public string? Cor { get; set; } = null;

        [MaxLength(10)]
        public string? VersaoSistema { get; set; } = null;

        public Veiculo() { }
    }
}
