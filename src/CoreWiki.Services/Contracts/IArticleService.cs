namespace CoreWiki.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<TModel> FindBySlugAsync<TModel>(string slug);

        Task<IList<TModel>> GetAllArticlesAsync<TModel>();

        Task<IEnumerable<TModel>> GetAllArticlesAsync<TModel>(int pageNumber, int pageSize);

        Task<IEnumerable<TModel>> GetLatestArticle<TModel>();

        int GetCount();

        Task Delete(string slug);
    }
}