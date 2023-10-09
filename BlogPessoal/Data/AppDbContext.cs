using BlogPessoal.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Postagem>().ToTable("tb_postagens");
            modelBuilder.Entity<Tema>().ToTable("tb_temas");
            modelBuilder.Entity<User>().ToTable("tb_usuarios");

            _ = modelBuilder.Entity<Postagem>()
             .HasOne(_ => _.Tema)
             .WithMany(t => t.Postagem)
             .HasForeignKey("TemaId")
             .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder.Entity<Postagem>()
             .HasOne(_ => _.Usuario)
             .WithMany(u => u.Postagem)
             .HasForeignKey("UserId")
             .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Postagem> Postagens { get; set; } = null!;
        public DbSet<Tema> Temas { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var currentTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, timeZoneInfo);
            var currentTimeUtc = currentTime.ToUniversalTime();
            var insertedEntries = this.ChangeTracker.Entries()
                                    .Where(x => x.State == EntityState.Added)
                                    .Select(x => x.Entity);
            foreach (var insertedEntry in insertedEntries)
            {
                if (insertedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = currentTimeUtc;
                }
            }
            var modifiedEntries = ChangeTracker.Entries()
                                    .Where(x => x.State == EntityState.Modified)
                                    .Select(x => x.Entity);
            foreach (var modifiedEntry in modifiedEntries)
            {
                if (modifiedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = currentTimeUtc;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
