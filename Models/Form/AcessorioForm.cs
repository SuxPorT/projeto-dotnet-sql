using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_dotnet_sql.Models.Form
{
    public class AcessorioForm
    {
        [ForeignKey("VeiculoNumeroChassi")]
        [Required(ErrorMessage = "O campo \"veiculoNumeroChassi\" é necessário")]
        [MaxLength(17, ErrorMessage = "O numero do chassi deve possuir no máximo 17 caracteres")]
        public string? VeiculoNumeroChassi { get; set; }

        [Required(ErrorMessage = "O campo \"descricao\" é necessário")]
        [MaxLength(50, ErrorMessage = "A descricao deve possuir no máximo 50 caracteres")]
        public string? Descricao { get; set; }

        public AcessorioForm() { }

        public Acessorio ToAcessorio()
        {
            return new Acessorio
            {
                Descricao = this.Descricao,
                VeiculoNumeroChassi = this.VeiculoNumeroChassi
            };
        }
    }
}
