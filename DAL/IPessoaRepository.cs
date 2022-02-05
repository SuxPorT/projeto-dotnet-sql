using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.DAL
{
    public interface IPessoaRepository : IDisposable
    {
        IEnumerable<Pessoa> GetPessoas();
        Pessoa GetPessoaPorCpfCnpj(string pessoaCpfCnpj);
        void Save();
    }
}
