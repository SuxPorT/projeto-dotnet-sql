using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models.Form
{
    public class VeiculoForm
    {
        [Key]
        public string? NumeroChassi { get; set; }

        [ForeignKey("CpfCnpj")]
        public string? ProprietarioCpfCnpj { get; set; }

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
            return new Veiculo
            {
                NumeroChassi = this.NumeroChassi,
                Modelo = this.Modelo,
                Ano = this.Ano,
                Cor = this.Cor,
                Valor = this.Valor,
                Quilometragem = this.Quilometragem,
                VersaoSistema = this.VersaoSistema,
                ProprietarioCpfCnpj = this.ProprietarioCpfCnpj
            };
        }
    }
}
