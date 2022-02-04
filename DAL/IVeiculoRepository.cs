using projeto_dotnet_sql.Models;
using projeto_dotnet_sql.Models.Form;

namespace projeto_dotnet_sql.DAL
{
    public interface IVeiculoRepository : IDisposable
    {
        IEnumerable<Veiculo> GetVeiculos();
        Veiculo GetVeiculoPorNumeroChassi(string numeroChassi);
        Veiculo GetUltimoVeiculo();
        void InsertVeiculo(Veiculo veiculo);
        void UpdateVeiculo(Veiculo veiculo, VeiculoForm veiculoAtualizado);
        void DeleteVeiculo(string numeroChassi);
        void Save();
    }
}
