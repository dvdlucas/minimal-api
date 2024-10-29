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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>().HasData(
                new Administrador
                {
                    Id = 1,
                    Email = "administrador@teste.com",
                    Senha = "123456",
                    Perfil = "ADM"
                }
            );
        }

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
