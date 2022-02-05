using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.DAL
{
    public class PessoaRepository : IPessoaRepository, IDisposable
    {
        private ConcessionariaContext context;
        private bool disposed = false;

        public PessoaRepository(ConcessionariaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Pessoa> GetPessoas()
        {
            return this.context.Pessoas!.ToList();
        }

        public Pessoa GetPessoaPorCpfCnpj(string pessoaCpfCnpj)
        {
            return this.context.Pessoas!.Find(pessoaCpfCnpj)!;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
