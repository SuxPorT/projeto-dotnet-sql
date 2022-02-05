using System.ComponentModel.DataAnnotations;

namespace projeto_dotnet_sql.Models
{
    public class Acessorio
    {
        [Key]
        public int AcessorioId { get; set; }

        public string? Descricao { get; set; }

        public Acessorio() { }
    }
}
