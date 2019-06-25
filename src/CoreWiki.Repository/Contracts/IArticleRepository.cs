namespace CoreWiki.Repository.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IArticleRepository : IRepository<Article>
    {
        bool IsArticleExistByTopic(string topic);

        Task<Article> FindBySlugAsync(string slug);

        Task<IList<Article>> All();

        Task UpdateAsync(Article article);

        void Delete(string slug);
    }
}