using FIAP_Cloud_Games.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAP_Cloud_Games.Infra.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jogo> Jogos { get; set; }

        public DbSet<JogosUsuarios> JogosUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<JogosUsuarios>()
                .HasKey(u => new { u.IdUsuario, u.IdJogo }); 

           
        }

    }
}
