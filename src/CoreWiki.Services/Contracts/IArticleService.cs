namespace CoreWiki.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<TModel> FindBySlugAsync<TModel>(string slug);

        Task<IEnumerable<TModel>> GetAllArticlesAsync<TModel>(int pageNumber, int pageSize);

        int GetCount();

        Task Delete(string slug);
    }
}