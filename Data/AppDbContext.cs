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
            modelBuilder.Entity<Postagem>().ToTable("tb_postagem");
        }

        public DbSet<Postagem> Postagens { get; set; } = null;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = this.ChangeTracker.Entries()
                                    .Where(x => x.State == EntityState.Detached)
                                    .Select(x => x.Entity);
            foreach(var insertedEntry in insertedEntries)
            {
                if (insertedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = DateTimeOffset.Now;
                }
            }
            var modifiedEntries = ChangeTracker.Entries()
                                    .Where(x => x.State == EntityState.Modified)
                                    .Select(x => x.Entity);
            foreach(var modifiedEntry in modifiedEntries) 
            {
                if (modifiedEntry is Auditable auditableEntity) 
                {
                    auditableEntity.Data = DateTimeOffset.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
