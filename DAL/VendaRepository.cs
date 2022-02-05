using projeto_dotnet_sql.Models;

namespace projeto_dotnet_sql.DAL
{
    public class VendaRepository : IVendaRepository, IDisposable
    {
        private ConcessionariaContext context;
        private bool disposed = false;

        public VendaRepository(ConcessionariaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Venda> GetVendas()
        {
            return this.context.Vendas!.ToList();
        }

        public Venda GetVendaPorID(int vendaId)
        {
            return this.context.Vendas!.Find(vendaId)!;
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
