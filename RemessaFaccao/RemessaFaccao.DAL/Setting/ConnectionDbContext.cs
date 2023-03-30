using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RemessaFaccao.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RemessaFaccao.DAL.Setting
{
    public class ConnectionDbContext : DbContext
    {
        public DbSet<Faccao> Faccao { get; set; }
        public DbSet<Remessa> Remessa { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }

        private readonly ConnectionSetting _connection;

        public ConnectionDbContext(IOptions<ConnectionSetting> connection)
        {
            _connection = connection.Value; 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection.SQLString);
        }
    }
}
