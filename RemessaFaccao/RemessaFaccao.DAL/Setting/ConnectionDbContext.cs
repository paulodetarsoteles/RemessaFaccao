using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;

namespace RemessaFaccao.DAL.Setting
{
    public class ConnectionDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly ConnectionSetting _connection;

        public ConnectionDbContext(IOptions<ConnectionSetting> connection)
        {
            _connection = connection.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection.SQLString);
        }

        public DbSet<Faccao> Faccao { get; set; }

        public DbSet<Aviamento> Aviamento { get; set; }

        public DbSet<Remessa> Remessa { get; set; }

        public DbSet<AviamentoRemessa> AviamentoRemessa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AviamentoRemessa>(x =>
            {
                x.HasKey(y => new
                {
                    y.AviamentoId,
                    y.RemessaId
                });
            });
        }
    }
}
