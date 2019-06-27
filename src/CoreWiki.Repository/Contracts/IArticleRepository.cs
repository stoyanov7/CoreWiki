﻿namespace CoreWiki.Repository.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Models;

    public interface IArticleRepository : IRepository<Article>
    {
        bool IsArticleExistByTopic(string topic);

        IEnumerable<Article> Get(Expression<Func<Article, bool>> predicate);

        Task<Article> FindBySlugAsync(string slug);

        Task<IList<Article>> All();

        Task UpdateAsync(Article article);

        void Delete(string slug);
    }
}