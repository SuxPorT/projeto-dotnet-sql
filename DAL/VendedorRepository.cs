using projeto_dotnet_sql.DAL.Interfaces;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

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

        public Vendedor GetVendedorPorID(int vendedorId)
        {
            return this.context.Vendedores!.Find(vendedorId)!;
        }

        public Vendedor GetUltimoVendedor()
        {
            return this.context.Vendedores!.OrderBy(e => e.VendedorId)
                                           .Last<Vendedor>();
        }

        public void InsertVendedor(Vendedor vendedor)
        {
            this.context.Vendedores!.Add(vendedor);
            this.Save();
        }

        public void UpdateVendedor(Vendedor vendedor, VendedorForm vendedorAtualizado)
        {
            this.context.Entry(vendedor).CurrentValues
                        .SetValues(vendedorAtualizado);
            this.Save();
        }

        public void DeleteVendedor(int vendedorId)
        {
            var vendedor = this.context.Vendedores!.Find(vendedorId);

            context.Vendedores.Remove(vendedor!);
            this.Save();
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
