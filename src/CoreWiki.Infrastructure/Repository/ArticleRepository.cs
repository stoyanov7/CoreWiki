namespace CoreWiki.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Domain.Repository;
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using NodaTime;
    using Utilities;

    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IClock clock;

        public ArticleRepository(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IClock clock)
            : base(unitOfWork)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.clock = clock;
        }

        public override async Task AddAsync(Article article)
        {
            await base.AddAsync(article);

            this.UnitOfWork
                .Context
                .Set<ArticleHistory>()
                .Add(ArticleHistory.FromArticle(article));
        }

        public IQueryable<Article> Details()
        {
            return this.UnitOfWork.Context.Set<Article>();
        }

        public bool IsArticleExistByTopic(string topic)
        {
            return this.UnitOfWork
                .Context
                .Set<Article>()
                .Any(x => x.Topic == topic);
        }

        public IEnumerable<Article> Get(Expression<Func<Article, bool>> predicate)
            => this.UnitOfWork
                .Context
                .Set<Article>()
                .Where(predicate)
                .AsEnumerable();

        public async Task<Article> FindByAsync(string slug)
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

        public async Task<IEnumerable<Article>> All(int pageNumber, int pageSize)
        {
            return await this.UnitOfWork
                .Context
                .Set<Article>()
                .AsNoTracking()
                .OrderBy(a => a.Topic)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Article>> LatestArticle()
        {
            return await this.UnitOfWork
                .Context
                .Set<Article>()
                .Include(a => a.Comments)
                .OrderByDescending(x => x.Published)
                .Take(5)
                .ToListAsync(); ;
        }

        public int Count() => this.UnitOfWork.Context.Set<Article>().Count();

        public async Task UpdateAsync(Article article)
        {
            this.UnitOfWork
                .Context
                .Attach(article)
                .State = EntityState.Modified;

            var existingArticle = this.UnitOfWork.Context.Set<Article>()
                .AsNoTracking()
                .First(a => a.Id == article.Id);

            article.ViewCount = existingArticle.ViewCount;
            article.Version = existingArticle.Version + 1;

            article.Published = this.clock.GetCurrentInstant();
            article.Slug = UrlHelpers.UrlFriendly(article.Topic.ToLower());
            article.AuthorId = this.httpContextAccessor
                .HttpContext
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            this.UnitOfWork
                .Context
                .Set<ArticleHistory>()
                .Add(ArticleHistory.FromArticle(article));

            try
            {
                await this.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!this.IsArticleExistByTopic(article.Topic))
                {
                    throw new ArticleNotFoundException(ex.Message);
                }
                else
                {
                    throw;
                }
            }
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

        public async Task IncrementViewCount(string slug)
        {
            var article = this.UnitOfWork
                .Context
                .Set<Article>()
                .Single(a => a.Slug == slug);

            article.ViewCount++;

            await this.UnitOfWork.Context.SaveChangesAsync();
        }
    }
}