using System.ComponentModel.DataAnnotations;

namespace projeto_dotnet_sql.Models.DTO
{
    public class VeiculoDTO
    {
        [Key]
        public string? NumeroChassi { get; set; }

        [Required(ErrorMessage = "O campo \"Modelo\" é necessário")]
        [MaxLength(50)]
        public string? Modelo { get; set; }

        [Required(ErrorMessage = "O campo \"Ano\" é necessário")]
        [MaxLength(30)]
        public int Ano { get; set; }

        [MaxLength(30)]
        public string? Cor { get; set; } = null;

        [Required(ErrorMessage = "O campo \"Valor\" é necessário")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "O campo \"Quilometragem\" é necessário")]
        public double Quilometragem { get; set; }

        [MaxLength(10)]
        public string? VersaoSistema { get; set; } = null;

        public ProprietarioDTO? Proprietario { get; set; }

        public VeiculoDTO() { }
    }
}
