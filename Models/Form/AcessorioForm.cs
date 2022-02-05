using System.ComponentModel.DataAnnotations;

namespace projeto_dotnet_sql.Models.Form
{
    public class AcessorioForm
    {
        [Required(ErrorMessage = "O campo \"descricao\" é necessário")]
        [MaxLength(50, ErrorMessage = "A descrição deve possuir no máximo 50 caracteres")]
        public string? Descricao { get; set; }
        public AcessorioForm() { }

        public Acessorio ToAcessorio()
        {
            return new Acessorio
            {
                Descricao = this.Descricao
            };
        }
    }
}
