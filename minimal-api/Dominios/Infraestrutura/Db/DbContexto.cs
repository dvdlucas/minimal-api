using Microsoft.EntityFrameworkCore;
using minimal_api.Dominios.Entidades;

namespace minimal_api.Dominios.Infraestrutura.Db
{
    public class DbContexto : DbContext
    {
        private readonly IConfiguration _configuracaoAppSettings;
        public DbContexto(DbContextOptions<DbContexto> options, IConfiguration configuracaoAppSettings) : base(options)
        {
            _configuracaoAppSettings = configuracaoAppSettings;
        }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var stringConexão = _configuracaoAppSettings.GetConnectionString("mysql")?.ToString();
                if (!string.IsNullOrEmpty(stringConexão))
                {
                    optionsBuilder.UseMySql(stringConexão,
                 ServerVersion.AutoDetect(stringConexão));
                }
            }
        }
    }
}
