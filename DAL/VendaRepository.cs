using projeto_dotnet_sql.DAL.Interfaces;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

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

        public Venda GetUltimaVenda()
        {
            return this.context.Vendas!.OrderBy(e => e.VendaId)
                                       .Last<Venda>();
        }

        public void InsertVenda(Venda venda)
        {
            this.context.Vendas!.Add(venda);
            this.Save();
        }

        public void UpdateVenda(Venda venda, VendaForm vendaAtualizada)
        {
            this.context.Entry(venda).CurrentValues
                        .SetValues(vendaAtualizada);
            this.Save();
        }

        public void DeleteVenda(int vendaId)
        {
            var venda = this.context.Vendas!.Find(vendaId);

            context.Vendas.Remove(venda!);
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
