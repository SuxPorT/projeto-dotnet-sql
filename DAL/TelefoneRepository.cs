using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.DAL
{
    public class TelefoneRepository : ITelefoneRepository, IDisposable
    {
        private ConcessionariaContext context;
        private bool disposed = false;

        public TelefoneRepository(ConcessionariaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Telefone> GetTelefones()
        {
            return this.context.Telefones!.ToList();
        }

        public Telefone GetTelefonePorID(int telefoneId)
        {
            return this.context.Telefones!.Find(telefoneId)!;
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
