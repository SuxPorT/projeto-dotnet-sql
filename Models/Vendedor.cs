using System.ComponentModel.DataAnnotations;

namespace projeto_dotnet_sql
{
    public class Vendedor
    {
        [Key]
        public int VendedorId { get; set; }

        [Required(ErrorMessage = "O campo \"nome\" é necessário")]
        [MaxLength(50)]
        public string? Nome { get; set; }

        public double SalarioMinimo { get; set; }

        public Vendedor() { }
    }
}