using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.DAL.Interfaces
{
    public interface IVendaRepository : IDisposable
    {
        IEnumerable<Venda> GetVendas();
        Venda GetVendaPorID(int vendaId);
        Venda GetUltimaVenda();
        void InsertVenda(Venda venda);
        void UpdateVenda(Venda venda, VendaForm vendaAtualizada);
        void DeleteVenda(int vendaId);
        void Save();
    }
}
