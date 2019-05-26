namespace CoreWiki.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CoreWikiContext : DbContext
    {
        public CoreWikiContext()
        {
            
        }

        public CoreWikiContext(DbContextOptions<CoreWikiContext> options)
            :base(options)
        {
            
        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Article>()
                .HasIndex(a => a.Slug)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
