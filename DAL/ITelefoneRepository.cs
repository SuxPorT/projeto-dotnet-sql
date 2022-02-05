using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.DAL
{
    public interface ITelefoneRepository : IDisposable
    {
        IEnumerable<Telefone> GetTelefones();
        Telefone GetTelefonePorID(int telefoneId);
        void Save();
    }
}
