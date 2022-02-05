using projeto_dotnet_sql.DAL.Interfaces;
using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.DAL
{
    public class VeiculoRepository : IVeiculoRepository, IDisposable
    {
        private ConcessionariaContext context;
        private bool disposed = false;

        public VeiculoRepository(ConcessionariaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Veiculo> GetVeiculos()
        {
            return this.context.Veiculos!.ToList();
        }

        public Veiculo GetVeiculoPorNumeroChassi(string numeroChassi)
        {
            return this.context.Veiculos!.Find(numeroChassi)!;
        }

        public Veiculo GetUltimoVeiculo()
        {
            return this.context.Veiculos!.OrderBy(e => e.NumeroChassi)
                                         .Last<Veiculo>();
        }

        public void InsertVeiculo(Veiculo veiculo)
        {
            this.context.Veiculos!.Add(veiculo);
            this.Save();
        }

        public void UpdateVeiculo(Veiculo veiculo, VeiculoForm veiculoAtualizado)
        {
            this.context.Entry(veiculo).CurrentValues
                        .SetValues(veiculoAtualizado);
            this.Save();
        }

        public void DeleteVeiculo(string numeroChassi)
        {
            var veiculo = this.context.Veiculos!.Find(numeroChassi);

            context.Veiculos.Remove(veiculo!);
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
