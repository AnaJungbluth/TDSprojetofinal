
using GerenTaref.RazorPages.Pages.Model;
using Microsoft.EntityFrameworkCore;

namespace GerenTaref.RazorPages.Data {
    public class AppDbContext : DbContext
    {
        public DbSet<UsuarioModel>? Usuarios { get; set; }
        public DbSet<TarefaModel>? Tarefas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=tds.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            
            modelBuilder.Entity<UsuarioModel>().ToTable("Usuarios").HasKey(l => l.UsuarioID);
            modelBuilder.Entity<UsuarioModel>().Property(o => o.UsuarioID).ValueGeneratedOnAdd();

            modelBuilder.Entity<TarefaModel>().ToTable("Tarefas").HasKey(n => n.TarefaID);
            modelBuilder.Entity<TarefaModel>().Property(m => m.TarefaID).ValueGeneratedOnAdd();

            modelBuilder.Entity<TarefaModel>()
                .HasOne(e => e.Responsavel)
                .WithMany()
                .HasForeignKey("UsuarioID");
        }
    }
}  