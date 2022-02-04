using System.ComponentModel.DataAnnotations;

namespace projeto_dotnet_sql.Models
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

        public Vendedor(string nome, double salarioMinimo)
        {
            Nome = nome;
            SalarioMinimo = salarioMinimo;
        }
    }
}
