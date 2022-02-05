using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.DAL.Interfaces
{
    public interface ITelefoneRepository : IDisposable
    {
        IEnumerable<Telefone> GetTelefones();
        Telefone GetTelefonePorID(int telefoneId);
        Telefone GetUltimoTelefone();
        void InsertTelefone(Telefone telefone);
        void UpdateTelefone(Telefone telefone, TelefoneForm telefoneAtualizado);
        void DeleteTelefone(int telefoneId);
        void Save();
    }
}
