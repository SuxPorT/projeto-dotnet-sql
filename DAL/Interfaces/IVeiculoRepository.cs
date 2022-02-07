using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.DAL.Interfaces
{
    public interface IVeiculoRepository : IDisposable
    {
        IEnumerable<Veiculo> GetVeiculos();
        Veiculo GetVeiculoPorNumeroChassi(string numeroChassi);
        Veiculo GetUltimoVeiculo();
        Veiculo InsertVeiculo(Veiculo veiculo);
        void UpdateVeiculo(Veiculo veiculo, VeiculoForm veiculoAtualizado);
        void DeleteVeiculo(string numeroChassi);
        void Save();
    }
}
