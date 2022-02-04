using System.ComponentModel.DataAnnotations;

namespace projeto_dotnet_sql.Models.Form
{
    public class VeiculoForm
    {
        [Key]
        public string? NumeroChassi { get; set; }

        //[Foreign key]
        public int ProprietarioId { get; set; }

        [Required(ErrorMessage = "O campo \"Modelo\" é necessário")]
        [MaxLength(50)]
        public string? Modelo { get; set; }

        [Required(ErrorMessage = "O campo \"Ano\" é necessário")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "O campo \"Valor\" é necessário")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "O campo \"Quilometragem\" é necessário")]
        public double Quilometragem { get; set; }

        [MaxLength(30)]
        public string? Cor { get; set; } = null;

        [MaxLength(10)]
        public string? VersaoSistema { get; set; } = null;

        public VeiculoForm() { }

        public Veiculo ToVeiculo()
        {
            return new Veiculo(NumeroChassi!,
                               ProprietarioId,
                               Modelo!,
                               Ano,
                               Valor,
                               Quilometragem,
                               Cor!,
                               VersaoSistema!);
        }
    }
}
