using projeto_dotnet_sql.Controllers.DTO;
using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.DAL.Interfaces
{
    public interface IProprietarioRepository : IDisposable
    {
        IEnumerable<Proprietario> GetProprietarios();
        Proprietario GetProprietarioPorCpfCnpj(string proprietarioCpfCnpj);
        Proprietario GetUltimoProprietario();
        void InsertProprietario(Proprietario proprietario);
        void UpdateProprietario(Proprietario proprietario, ProprietarioForm proprietarioAtualizado);
        void DeleteProprietario(string cpfCnpj);
        void Save();
    }
}
