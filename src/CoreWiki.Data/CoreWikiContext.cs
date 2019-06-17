namespace CoreWiki.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Identity;

    public class CoreWikiContext : IdentityDbContext<ApplicationUser>
    {
        public CoreWikiContext(DbContextOptions<CoreWikiContext> options)
            :base(options)
        {
            
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ArticleHistory> ArticleHistories { get; set; }

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
