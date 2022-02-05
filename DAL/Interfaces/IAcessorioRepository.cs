using projeto_dotnet_sql.Controllers.DTO;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.DAL.Interfaces
{
    public interface IAcessorioRepository : IDisposable
    {
        IEnumerable<Acessorio> GetAcessorios();
        Acessorio GetAcessorioPorID(int acessorioId);
        Acessorio GetUltimoAcessorio();
        void InsertAcessorio(Acessorio proprietario);
        void UpdateAcessorio(Acessorio proprietario, AcessorioForm acessorioAtualizado);
        void DeleteAcessorio(int acessorioId);
        void Save();
    }
}
