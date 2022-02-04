
namespace projeto_dotnet_sql.DAL
{
    public interface IVeiculoRepository : IDisposable
    {
        IEnumerable<Veiculos> GetVeiculos();
        void Save();
    }
}