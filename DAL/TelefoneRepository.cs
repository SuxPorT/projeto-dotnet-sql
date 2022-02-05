using projeto_dotnet_sql.DAL.Interfaces;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

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

        public Telefone GetUltimoTelefone()
        {
            return this.context.Telefones!.OrderBy(e => e.TelefoneId)
                                          .Last<Telefone>();
        }

        public void InsertTelefone(Telefone telefone)
        {
            this.context.Telefones!.Add(telefone);
            this.Save();
        }

        public void UpdateTelefone(Telefone telefone, TelefoneForm telefoneAtualizado)
        {
            this.context.Entry(telefone).CurrentValues
                        .SetValues(telefoneAtualizado);
            this.Save();
        }

        public void DeleteTelefone(int telefoneId)
        {
            var telefone = this.context.Telefones!.Find(telefoneId);

            context.Telefones.Remove(telefone!);
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
