using projeto_dotnet_sql.DAL.Interfaces;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.DAL
{
    public class AcessorioRepository : IAcessorioRepository, IDisposable
    {
        private ConcessionariaContext context;
        private bool disposed = false;

        public AcessorioRepository(ConcessionariaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Acessorio> GetAcessorios()
        {
            return this.context.Acessorios!.ToList();
        }

        public Acessorio GetAcessorioPorID(int acessorioId)
        {
            return this.context.Acessorios!.Find(acessorioId)!;
        }

        public void InsertAcessorio(Acessorio acessorio)
        {
            this.context.Acessorios!.Add(acessorio);
            this.Save();
        }

        public Acessorio GetUltimoAcessorio()
        {
            return this.context.Acessorios!.OrderBy(e => e.AcessorioId)
                                           .Last<Acessorio>();
        }

        public void UpdateAcessorio(Acessorio acessorio, AcessorioForm acessorioAtualizado)
        {
            this.context.Entry(acessorio).CurrentValues
                        .SetValues(acessorioAtualizado);
            this.Save();
        }

        public void DeleteAcessorio(int acessorioId)
        {
            var acessorio = this.context.Acessorios!.Find(acessorioId);

            context.Acessorios.Remove(acessorio!);
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
