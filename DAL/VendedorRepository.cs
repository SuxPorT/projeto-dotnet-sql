using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.DAL
{
    public class VendedorRepository : IVendedorRepository, IDisposable
    {
        private ConcessionariaContext context;
        private bool disposed = false;

        public VendedorRepository(ConcessionariaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Vendedor> GetVendedores()
        {
            return this.context.Vendedores!.ToList();
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
