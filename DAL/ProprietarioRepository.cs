using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.DAL
{
    public class ProprietarioRepository : IProprietarioRepository, IDisposable
    {
        private ConcessionariaContext context;
        private bool disposed = false;

        public ProprietarioRepository(ConcessionariaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Proprietario> GetProprietarios()
        {
            return this.context.Proprietarios!.ToList();
        }

        public Proprietario GetProprietarioPorCpfCnpj(string proprietarioCpfCnpj)
        {
            return this.context.Proprietarios!.Find(proprietarioCpfCnpj)!;
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
