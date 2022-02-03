
namespace projeto_dotnet_sql.DAL
{
    public interface IVendedorRepository : IDisposable
    {
        IEnumerable<Vendedor> GetVendedores();
        void Save();
    }
}
