using Microsoft.EntityFrameworkCore;

namespace projeto_dotnet_sql.Models
{
    public class ConcessionariaContext : DbContext
    {
        public DbSet<Vendedor>? Vendedores { get; set; } = null;
        public DbSet<Veiculos>? Veiculos { get; set; } = null;

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
