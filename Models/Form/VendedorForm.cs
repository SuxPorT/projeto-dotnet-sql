using System.ComponentModel.DataAnnotations;

namespace projeto_dotnet_sql.Models.Form
{
    public class VendedorForm
    {
        [Required(ErrorMessage = "O campo \"nome\" é necessário")]
        [MaxLength(50)]
        public string? Nome { get; set; }

        [Required]
        [Range(1200, 6000, ErrorMessage = "O salário mínimo deve estar entre R$1200,00 e R$6000,00")]
        public double SalarioBase { get; set; }

        public VendedorForm() { }

        public Vendedor ToVendedor()
        {
            return new Vendedor
            {
                Nome = this.Nome,
                SalarioBase = this.SalarioBase
            };
        }
    }
}
