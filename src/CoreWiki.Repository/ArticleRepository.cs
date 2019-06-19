namespace CoreWiki.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool IsArticleExistByTopic(string topic)
        {
            return this.UnitOfWork
                .Context
                .Set<Article>()
                .Any(x => x.Topic == topic);
        }

        public async Task<Article> FindBySlugAsync(string slug)
        {
            return await this.UnitOfWork
                .Context
                .Set<Article>()
                .FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<IList<Article>> All()
        {
            return await this.UnitOfWork
                .Context
                .Set<Article>()
                .Include(c => c.Comments)
                .ToListAsync();
        }

        public void Delete(string slug)
        {
            var existing = this.UnitOfWork
                .Context
                .Set<Article>()
                .FirstOrDefault(x => x.Slug == slug);

            if (existing != null)
            {
                this.UnitOfWork
                    .Context
                    .Set<Article>()
                    .Remove(existing);
            }
        }
    }
}