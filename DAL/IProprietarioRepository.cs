using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.DAL
{
    public interface IProprietarioRepository : IDisposable
    {
        IEnumerable<Proprietario> GetProprietarios();
        Proprietario GetProprietarioPorCpfCnpj(string proprietarioCpfCnpj);
        void Save();
    }
}
