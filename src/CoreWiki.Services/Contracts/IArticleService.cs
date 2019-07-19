namespace CoreWiki.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IArticleService
    {
        Task Create(string topic, string content, string authorId);

        Task<TModel> DetailsAsync<TModel>(string slug);

        Task<TModel> DetailsAsync<TModel>(Comment comment);

        bool IsArticleExist(string topic);

        Task<TModel> FindBySlugAsync<TModel>(string slug);

        Task<IList<TModel>> GetAllArticlesAsync<TModel>();

        Task<IEnumerable<TModel>> GetAllArticlesAsync<TModel>(int pageNumber, int pageSize);

        Task<IEnumerable<TModel>> GetLatestArticle<TModel>();

        Task<TModel> GetArticleWithHistoryDetails<TModel>(string slug);

        Task<TModel> GetArticleHistoryAndAuthor<TModel>(string slug);

        Task<TModel[]> GetHistory<TModel>(string[] compare);

        int GetCount();

        Task Delete(string slug);

        Task CanAuthorBeNotified(string authorId, string encodedUrl);
    }
}