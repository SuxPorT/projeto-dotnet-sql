using System.ComponentModel.DataAnnotations;

namespace projeto_dotnet_sql.Models.Form
{
    public class VendedorForm
    {
        [Required(ErrorMessage = "O campo \"nome\" é necessário")]
        [MaxLength(50)]
        public string? Nome { get; set; }

        public double SalarioMinimo { get; set; }

        public VendedorForm() { }

        public Vendedor ToVendedor()
        {
            return new Vendedor(Nome!, SalarioMinimo);
        }
    }
}
