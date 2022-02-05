using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.DAL
{
    public interface IVendaRepository : IDisposable
    {
        IEnumerable<Venda> GetVendas();
        Venda GetVendaPorID(int vendaId);
        void Save();
    }
}
