using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.DAL
{
    public interface IVendedorRepository : IDisposable
    {
        IEnumerable<Vendedor> GetVendedores();
        Vendedor GetVendedorPorID(int vendedorId);
        Vendedor GetUltimoVendedor();
        void InsertVendedor(Vendedor vendedor);
        void UpdateVendedor(Vendedor vendedor, VendedorForm vendedorAtualizado);
        void DeleteVendedor(int vendedorId);
        void Save();
    }
}
