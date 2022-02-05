using Microsoft.EntityFrameworkCore;

namespace projeto_dotnet_sql.Models
{
    public class ConcessionariaContext : DbContext
    {
        public DbSet<Proprietario>? Proprietarios { get; set; } = null;
        public DbSet<Telefone>? Telefones { get; set; } = null;
        public DbSet<Veiculo>? Veiculos { get; set; } = null;
        public DbSet<Acessorio>? Acessorios { get; set; } = null;
        public DbSet<Vendedor>? Vendedores { get; set; } = null;
        public DbSet<Venda>? Vendas { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional:
            true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("SqlServer");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
