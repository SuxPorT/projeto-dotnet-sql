using projeto_dotnet_sql.Controllers.DTO;
using projeto_dotnet_sql.DAL.Interfaces;
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

        public void InsertProprietario(Proprietario proprietario)
        {
            this.context.Proprietarios!.Add(proprietario);
            this.Save();
        }

        public Proprietario GetUltimoProprietario()
        {
            return this.context.Proprietarios!.OrderBy(e => e.CpfCnpj)
                                              .Last<Proprietario>();
        }

        public void UpdateProprietario(Proprietario proprietario, ProprietarioForm proprietarioAtualizado)
        {
            this.context.Entry(proprietario).CurrentValues
                        .SetValues(proprietarioAtualizado);
            this.Save();
        }

        public void DeleteProprietario(string cpfCnpj)
        {
            var proprietario = this.context.Proprietarios!.Find(cpfCnpj);

            context.Proprietarios.Remove(proprietario!);
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
